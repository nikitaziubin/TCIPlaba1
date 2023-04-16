using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;
using TCIPlaba1.NewFolder;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;

namespace TCIPlaba1.Controllers
{
	public class ParticipantsController : Controller
	{
		private readonly Ictplaba1Context _context;

		public ParticipantsController(Ictplaba1Context context)
		{
			_context = context;
		}

		// GET: Participants
		public async Task<IActionResult> Index()
		{
			var ictplaba1Context = await _context.Participants.Include(p => p.MatchNavigation).Include(p => p.TeamNavigation).Include(p => p.TeamRoleNavigation).ToListAsync();
			var matchconex = await _context.Matches.Include(p => p.DivisionNavigation).Include(p => p.StadiumNavigation).ToListAsync();
			var model = new MatchAndParticipantsVM()
			{
				match = matchconex,
				participant = ictplaba1Context
			};
			return View(model);
		}

		// GET: Participants/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Participants == null)
			{
				return NotFound();
			}
			//var division = await _context.Divisions.Include(p => p.Matches).ToListAsync();
			//var stadium = await _context.Stadia.Include(p => p.Matches).ToListAsync();
			var match = await _context.Matches.Include(p => p.DivisionNavigation).Include(p => p.StadiumNavigation).FirstOrDefaultAsync(m => m.Id == id);


			if (match == null)
			{
				return NotFound();
			}

			return View(match);
		}

        // GET: Participants/Create
        
        public IActionResult Create()
		{
			ViewData["Match"] = new SelectList(_context.Matches, "Id", "Id");
			ViewData["Team"] = new SelectList(_context.Teams, "Id", "Id");
			ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Id");
			return View();
		}

		// POST: Participants/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Match,Team,TeamRole,Goals")] Participant participant)
		{
			_context.Add(participant);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
			//if (ModelState.IsValid)
			//{

			//}
			//ViewData["Match"] = new SelectList(_context.Matches, "Id", "Id", participant.Match);
			//ViewData["Team"] = new SelectList(_context.Teams, "Id", "Id", participant.Team);
			//ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Id", participant.TeamRole);
			//return View(participant);
		}

        // GET: Participants/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Participants == null)
			{
				return NotFound();
			}

			var participant = await _context.Participants.FindAsync(id);
			if (participant == null)
			{
				return NotFound();
			}
			ViewData["Team"] = new SelectList(_context.Teams, "Id", "Name", participant.Team);
			//ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Name", participant.TeamRole);
			return View(participant);
		}

        // POST: Participants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Match,Team,TeamRole,Goals")] Participant participant)
		{
			if (id != participant.Id)
			{
				return NotFound();
			}

			_context.Update(participant);
			await _context.SaveChangesAsync();
			
			ViewData["Team"] = new SelectList(_context.Teams, "Id", "Name", participant.Team);
			ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Name", participant.TeamRole);
			_context.Update(participant);
			_context.SaveChanges();

			//return View( participant);
			return RedirectToAction("Index", "Participants");
		}

        // GET: Participants/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Participants == null)
			{
				return NotFound();
			}

			var match = await _context.Matches
				.Include(p => p.StadiumNavigation)
				.Include(p => p.DivisionNavigation)
				.Include(p => p.Participants)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (match == null)
			{
				return NotFound();
			}

			return View(match);
		}

        // POST: Participants/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Participants == null)
			{
				return Problem("Entity set 'Ictplaba1Context.Participants'  is null.");
			}
			var match = await _context.Matches.FindAsync(id);
			if (match != null)
			{
				_context.Matches.Remove(match);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ParticipantExists(int id)
		{
			return (_context.Participants?.Any(e => e.Id == id)).GetValueOrDefault();
		}

        [Authorize(Roles = "admin")]
		[HttpPost, ActionName("Import")]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
		{
			if (ModelState.IsValid)
			{
				if (fileExcel != null)
				{
					using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
					{
						await fileExcel.CopyToAsync(stream);
						//using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
						using (XLWorkbook workBook = new XLWorkbook(stream))
						{
							// Iterate over all worksheets (in this case, categories)
							foreach (IXLWorksheet worksheet in workBook.Worksheets)
							{
								foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
								{
									try
									{
										string stadiumName = row.Cell(1).Value.ToString();
										Stadium? s = _context.Stadia.FirstOrDefault(s => s.Name == stadiumName);
                                        if (s != null)
										{

										}
										else
										{
											Stadium stadium = new Stadium();
                                            stadium.Name = row.Cell(1).Value.ToString();
                                            stadium.Address = row.Cell(2).Value.ToString();
                                            string Cpapacity = row.Cell(3).Value.ToString();
                                            stadium.Capacity = Convert.ToInt32(Cpapacity);
                                            _context.Stadia.Add(stadium);
                                        }
										
									}
									catch (Exception e)
									{
										// Log the exception
										//_logger.LogError(e, "Error importing row");
									}
								}
							}
						}
					}
				}
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));

		}
		public ActionResult Export()
		{
			using (XLWorkbook workbook = new XLWorkbook())
			{
				var matchse = _context.Matches.Include(p=>p.Participants).ToList();
				var teams = _context.Teams.ToList();
				// Тут, для прикладу ми пишемо усі книжки з БД, в своїх проєктах ТАК НЕ РОБИТИ (писати лише вибрані)

				var worksheet = workbook.Worksheets.Add("Match");
				worksheet.Cell("A1").Value = "Ім'я прешої команди";
				worksheet.Cell("B1").Value = "Голи першої команди";
				worksheet.Cell("D1").Value = "Голи Другої команди";
				worksheet.Cell("C1").Value = "Ім'я Другої команди";
				worksheet.Row(1).Style.Font.Bold = true;
				int j = 2; 
				foreach (var c in matchse)
				{
					var participants = c.Participants.ToList();
					int i = 1;
					var teamName1 = teams.Where(t => t.Id == participants[0].Team).Select(t => t.Name).FirstOrDefault();
					worksheet.Cell(j, i).Value = teamName1;
					worksheet.Cell(j, i + 1).Value = participants[0].Goals;
					i += 2;
					var teamName2 = teams.Where(t => t.Id == participants[1].Team).Select(t => t.Name).FirstOrDefault();
					worksheet.Cell(j, i).Value = participants[1].Goals;
					worksheet.Cell(j, i+1).Value = teamName2;
					i += 2;
					j++;
				}
				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					stream.Flush();
					return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
					{
						// Змініть назву файла відповідно до тематики Вашого проєкту
						FileDownloadName = $"library{DateTime.UtcNow.ToShortDateString()}.xlsx"
					};
				}
			}
		}
	}
}