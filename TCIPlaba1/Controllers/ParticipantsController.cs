using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;
using TCIPlaba1.NewFolder;

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
            
            if (ModelState.IsValid)
            {
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Match"] = new SelectList(_context.Matches, "Id", "Id", participant.Match);
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Id", participant.Team);
            ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Id", participant.TeamRole);
            return View(participant);
        }

        // GET: Participants/Edit/5
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
            ViewData["Match"] = new SelectList(_context.Matches, "Id", "Id", participant.Match);
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Id", participant.Team);
            ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Id", participant.TeamRole);
            return View(participant);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Match,Team,TeamRole,Goals")] Participant participant)
        {
            if (id != participant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantExists(participant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Match"] = new SelectList(_context.Matches, "Id", "Id", participant.Match);
            ViewData["Team"] = new SelectList(_context.Teams, "Id", "Id", participant.Team);
            ViewData["TeamRole"] = new SelectList(_context.TeamRoles, "Id", "Id", participant.TeamRole);
            //var match = new Match();
            //match.Participants.Where(p => p.Id == id).FirstOrDefault().Goals = participant.Goals;
            //var particcipantID = _context.Model.
            //participant.Goals = participant.Goals;
            _context.Update(participant);
            _context.SaveChanges();
           
            //return View( participant);
            return RedirectToAction("Index", "Participants");
        }

        // GET: Participants/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Participants == null)
            {
                return Problem("Entity set 'Ictplaba1Context.Participants'  is null.");
            }
            var match= await _context.Matches.FindAsync(id);
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
    }
}
