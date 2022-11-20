using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jalgratta.Controllers
{
    public class TeenusController : Controller
    {
        private readonly DataBase _context;

        public TeenusController(DataBase context)
        {
            _context = context;
        }

        // GET: Teenus
        public async Task<IActionResult> Index()
        {
              return View(await _context.Teenus.ToListAsync());
        }

        // GET: Teenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teenus == null)
            {
                return NotFound();
            }

            var teenus = await _context.Teenus
                .FirstOrDefaultAsync(m => m.TeenusId == id);
            if (teenus == null)
            {
                return NotFound();
            }

            return View(teenus);
        }

        // GET: Teenus/Create
        [Authorize(Policy = "readpolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "readpolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeenusId,Info,Hind")] Teenus teenus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teenus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teenus);
        }

        // GET: Teenus/Edit/5
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teenus == null)
            {
                return NotFound();
            }

            var teenus = await _context.Teenus.FindAsync(id);
            if (teenus == null)
            {
                return NotFound();
            }
            return View(teenus);
        }

        // POST: Teenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("TeenusId,Info,Hind")] Teenus teenus)
        {
            if (id != teenus.TeenusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teenus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeenusExists(teenus.TeenusId))
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
            return View(teenus);
        }
        [Authorize(Policy = "readpolicy")]
        // GET: Teenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teenus == null)
            {
                return NotFound();
            }

            var teenus = await _context.Teenus
                .FirstOrDefaultAsync(m => m.TeenusId == id);
            if (teenus == null)
            {
                return NotFound();
            }

            return View(teenus);
        }

        // POST: Teenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teenus == null)
            {
                return Problem("Entity set 'DataBase.Teenus'  is null.");
            }
            var teenus = await _context.Teenus.FindAsync(id);
            if (teenus != null)
            {
                _context.Teenus.Remove(teenus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeenusExists(int id)
        {
          return _context.Teenus.Any(e => e.TeenusId == id);
        }
    }
}
