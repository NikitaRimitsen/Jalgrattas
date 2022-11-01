using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta.Data;
using Jalgratta.Models;

namespace Jalgratta.Controllers
{
    public class TootajadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TootajadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tootajads
        public async Task<IActionResult> Index()
        {
              return View(await _context.Tootajad.ToListAsync());
        }

        // GET: Tootajads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tootajad == null)
            {
                return NotFound();
            }

            var tootajad = await _context.Tootajad
                .FirstOrDefaultAsync(m => m.TootajadId == id);
            if (tootajad == null)
            {
                return NotFound();
            }

            return View(tootajad);
        }

        // GET: Tootajads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tootajads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TootajadId,Nimi,Vanus,Staaz,Email,Telefon")] Tootajad tootajad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tootajad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tootajad);
        }

        // GET: Tootajads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tootajad == null)
            {
                return NotFound();
            }

            var tootajad = await _context.Tootajad.FindAsync(id);
            if (tootajad == null)
            {
                return NotFound();
            }
            return View(tootajad);
        }

        // POST: Tootajads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TootajadId,Nimi,Vanus,Staaz,Email,Telefon")] Tootajad tootajad)
        {
            if (id != tootajad.TootajadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tootajad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TootajadExists(tootajad.TootajadId))
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
            return View(tootajad);
        }

        // GET: Tootajads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tootajad == null)
            {
                return NotFound();
            }

            var tootajad = await _context.Tootajad
                .FirstOrDefaultAsync(m => m.TootajadId == id);
            if (tootajad == null)
            {
                return NotFound();
            }

            return View(tootajad);
        }

        // POST: Tootajads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tootajad == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tootajad'  is null.");
            }
            var tootajad = await _context.Tootajad.FindAsync(id);
            if (tootajad != null)
            {
                _context.Tootajad.Remove(tootajad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TootajadExists(int id)
        {
          return _context.Tootajad.Any(e => e.TootajadId == id);
        }
    }
}
