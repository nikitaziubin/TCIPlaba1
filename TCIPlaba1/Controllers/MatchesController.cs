﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;

namespace TCIPlaba1.Controllers
{
    public class MatchesController : Controller
    {
        private readonly Ictplaba1Context _context;

        public MatchesController(Ictplaba1Context context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var ictplaba1Context = _context.Matches.Include(m => m.DivisionNavigation).Include(m => m.StadiumNavigation);
            return View(await ictplaba1Context.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.DivisionNavigation)
                .Include(m => m.StadiumNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["Division"] = new SelectList(_context.Divisions, "Id", "Id");
            ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Id");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Division,Stadium")] Match match)
        {

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //}
            //ViewData["Division"] = new SelectList(_context.Divisions, "Id", "Id", match.Division);
            //ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Id", match.Stadium);

            //return View(match);
        }

        public IActionResult CreateM()
        {
            ViewData["Division"] = new SelectList(_context.Divisions, "Id", "Id");
            ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Id");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateM([Bind("Id,Date,Division,Stadium")] Match match)
        {

            _context.Add(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //}
            //ViewData["Division"] = new SelectList(_context.Divisions, "Id", "Id", match.Division);
            //ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Id", match.Stadium);

            //return View(match);
        }


        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["Division"] = new SelectList(_context.Divisions, "Id", "Id", match.Division);
            ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Id", match.Stadium);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Division,Stadium")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
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
            ViewData["Division"] = new SelectList(_context.Divisions, "Id", "Id", match.Division);
            ViewData["Stadium"] = new SelectList(_context.Stadia, "Id", "Id", match.Stadium);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Matches == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.DivisionNavigation)
                .Include(m => m.StadiumNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Matches == null)
            {
                return Problem("Entity set 'Ictplaba1Context.Matches'  is null.");
            }
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
          return (_context.Matches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
