using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;

namespace TCIPlaba1.Controllers
{
    [Authorize(Roles = "admin")]
    public class StadiaController : Controller
    {
        private readonly Ictplaba1Context _context;

        public StadiaController(Ictplaba1Context context)
        {
            _context = context;
        }

        // GET: Stadia
        public async Task<IActionResult> Index()
        {
              return _context.Stadia != null ? 
                          View(await _context.Stadia.ToListAsync()) :
                          Problem("Entity set 'Ictplaba1Context.Stadia'  is null.");
        }

        // GET: Stadia/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Stadia == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // GET: Stadia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stadia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Capacity,MaxCapacity")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadium);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Participants");
            }
            return View(stadium);
        }

        // GET: Stadia/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Stadia == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadia.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            return View(stadium);
        }

        // POST: Stadia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name,Address,Capacity,MaxCapacity")] Stadium stadium)
        {
            if (id != stadium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadiumExists(stadium.Id))
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
            return View(stadium);
        }

        // GET: Stadia/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Stadia == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // POST: Stadia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Stadia == null)
            {
                return Problem("Entity set 'Ictplaba1Context.Stadia'  is null.");
            }
            var stadium = await _context.Stadia.FindAsync(id);
            if (stadium != null)
            {
                _context.Stadia.Remove(stadium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadiumExists(byte id)
        {
          return (_context.Stadia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
