using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta.Models;

namespace Jalgratta.Controllers
{
    public class TeenusetelimusController : Controller
    {
        private readonly DataBase _context;

        public TeenusetelimusController(DataBase context)
        {
            _context = context;
        }

        // GET: Teenusetelimus
        public async Task<IActionResult> Index()
        {
            var dataBase = _context.Teenusetelimus.Include(t => t.Kasutaja).Include(t => t.Teenus).Include(t => t.Tootajad);
            return View(await dataBase.ToListAsync());
        }

        // GET: Teenusetelimus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teenusetelimus == null)
            {
                return NotFound();
            }

            var teenusetelimus = await _context.Teenusetelimus
                .Include(t => t.Kasutaja)
                .Include(t => t.Teenus)
                .Include(t => t.Tootajad)
                .FirstOrDefaultAsync(m => m.TelimusId == id);
            if (teenusetelimus == null)
            {
                return NotFound();
            }

            return View(teenusetelimus);
        }

        // GET: Teenusetelimus/Create
        public IActionResult Create()
        {
            ViewData["KasutajaId"] = new SelectList(_context.Kasutaja, "KasutajaId", "Perekonnanimi");
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info");
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi");
            return View();
        }

        // POST: Teenusetelimus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TelimusId,TootajadId,TeenusId,KasutajaId,Kuupaev")] Teenusetelimus teenusetelimus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teenusetelimus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KasutajaId"] = new SelectList(_context.Kasutaja, "KasutajaId", "Perekonnanimi", teenusetelimus.KasutajaId);
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimus.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimus.TootajadId);
            return View(teenusetelimus);
        }

        // GET: Teenusetelimus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teenusetelimus == null)
            {
                return NotFound();
            }

            var teenusetelimus = await _context.Teenusetelimus.FindAsync(id);
            if (teenusetelimus == null)
            {
                return NotFound();
            }
            ViewData["KasutajaId"] = new SelectList(_context.Kasutaja, "KasutajaId", "Perekonnanimi", teenusetelimus.KasutajaId);
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimus.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimus.TootajadId);
            return View(teenusetelimus);
        }

        // POST: Teenusetelimus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TelimusId,TootajadId,TeenusId,KasutajaId,Kuupaev")] Teenusetelimus teenusetelimus)
        {
            if (id != teenusetelimus.TelimusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teenusetelimus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeenusetelimusExists(teenusetelimus.TelimusId))
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
            ViewData["KasutajaId"] = new SelectList(_context.Kasutaja, "KasutajaId", "Perekonnanimi", teenusetelimus.KasutajaId);
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimus.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimus.TootajadId);
            return View(teenusetelimus);
        }

        // GET: Teenusetelimus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teenusetelimus == null)
            {
                return NotFound();
            }

            var teenusetelimus = await _context.Teenusetelimus
                .Include(t => t.Kasutaja)
                .Include(t => t.Teenus)
                .Include(t => t.Tootajad)
                .FirstOrDefaultAsync(m => m.TelimusId == id);
            if (teenusetelimus == null)
            {
                return NotFound();
            }

            return View(teenusetelimus);
        }

        // POST: Teenusetelimus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teenusetelimus == null)
            {
                return Problem("Entity set 'DataBase.Teenusetelimus'  is null.");
            }
            var teenusetelimus = await _context.Teenusetelimus.FindAsync(id);
            if (teenusetelimus != null)
            {
                _context.Teenusetelimus.Remove(teenusetelimus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeenusetelimusExists(int id)
        {
          return _context.Teenusetelimus.Any(e => e.TelimusId == id);
        }
    }
}
