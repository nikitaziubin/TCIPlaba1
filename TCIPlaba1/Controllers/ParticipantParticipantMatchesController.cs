using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;

namespace TCIPlaba1.Controllers
{
    public class ParticipantParticipantMatchesController : Controller
    {
        private readonly Ictplaba1Context _context;

        public ParticipantParticipantMatchesController(Ictplaba1Context context)
        {
            _context = context;
        }

        // GET: ParticipantParticipantMatches
        public async Task<IActionResult> Index()
        {
            //return _context.ParticipantParticipantMatch != null ? 
            //            View(await _context.ParticipantParticipantMatch.ToListAsync()) :
            //            Problem("Entity set 'Ictplaba1Context.ParticipantParticipantMatch'  is null.");
            var ictplaba1Context = _context.Matches.Include(m => m.DivisionNavigation).Include(m => m.StadiumNavigation);
            return View(await ictplaba1Context.ToListAsync());
        }

        // GET: ParticipantParticipantMatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParticipantParticipantMatch == null)
            {
                return NotFound();
            }

            var participantParticipantMatch = await _context.ParticipantParticipantMatch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantParticipantMatch == null)
            {
                return NotFound();
            }

            return View(participantParticipantMatch);
        }

        // GET: ParticipantParticipantMatches/Create
        public IActionResult Create()
        {
            //var divisions = _context.Divisions.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();
            //ViewBag.Divisions = divisions;
            ViewData["Divisions"] = new SelectList(_context.Divisions, "Id", "Name");
            ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Name");
            ViewData["Team1"] = new SelectList(_context.Teams, "Id", "Name");
			ViewData["Team2"] = new SelectList(_context.Teams, "Id", "Name");
			ViewData["TeamRole1"] = new SelectList(_context.TeamRoles, "Id", "Name");
			ViewData["TeamRole2"] = new SelectList(_context.TeamRoles, "Id", "Name");

			return View();
        }

        // POST: ParticipantParticipantMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Match,Team1,TeamRole1,Goals1,Team2,TeamRole2,Goals2,Date,Division,Stadium")] ParticipantParticipantMatch participantParticipantMatch)
        {
            Participant participant1 = new Participant();
            Participant participant2 = new Participant();
            Match match = new Match();
            participant1.Team = participantParticipantMatch.Team1;
            participant1.TeamRole = participantParticipantMatch.TeamRole1;
            participant1.Goals = participantParticipantMatch.Goals1;

            participant2.Team = participantParticipantMatch.Team2;
            participant2.TeamRole = participantParticipantMatch.TeamRole2;
            participant2.Goals = participantParticipantMatch.Goals2;

            match.Date = participantParticipantMatch.Date;
            match.Division = participantParticipantMatch.Division;
            match.Stadium = participantParticipantMatch.Stadium;
            _context.Add(match);
            _context.SaveChanges();
            participant1.Match = match.Id;
            participant2.Match = match.Id;
            _context.Add(participant1);

            _context.Add(participant2);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(ParticipantsController.Index));
            return Redirect($"/Participants/Details/{match.Id}");
            //if (ModelState.IsValid)
            //         {

            //         }
            //         return View(participantParticipantMatch);
        }

        // GET: ParticipantParticipantMatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParticipantParticipantMatch == null)
            {
                return NotFound();
            }

            var participantParticipantMatch = await _context.ParticipantParticipantMatch.FindAsync(id);
            if (participantParticipantMatch == null)
            {
                return NotFound();
            }
            return View(participantParticipantMatch);
        }

        // POST: ParticipantParticipantMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Match,Team1,TeamRole1,Goals1,Team2,TeamRole2,Goals2,Date,Division,Stadium")] ParticipantParticipantMatch participantParticipantMatch)
        {
            if (id != participantParticipantMatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participantParticipantMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantParticipantMatchExists(participantParticipantMatch.Id))
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
            return View(participantParticipantMatch);
        }

        // GET: ParticipantParticipantMatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParticipantParticipantMatch == null)
            {
                return NotFound();
            }

            var participantParticipantMatch = await _context.ParticipantParticipantMatch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participantParticipantMatch == null)
            {
                return NotFound();
            }

            return View(participantParticipantMatch);
        }

        // POST: ParticipantParticipantMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParticipantParticipantMatch == null)
            {
                return Problem("Entity set 'Ictplaba1Context.ParticipantParticipantMatch'  is null.");
            }
            var participantParticipantMatch = await _context.ParticipantParticipantMatch.FindAsync(id);
            if (participantParticipantMatch != null)
            {
                _context.ParticipantParticipantMatch.Remove(participantParticipantMatch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantParticipantMatchExists(int id)
        {
            return (_context.ParticipantParticipantMatch?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
