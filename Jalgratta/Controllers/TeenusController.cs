using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jalgratta.Data;
using Jalgratta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jalgratta.Controllers

{
    public class TeenusController : Controller
    {
        //.
        private readonly ApplicationDbContext _context;

        public TeenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get: teenus
        public async Task<IActionResult> index()
        {
            return View(await _context.Teenus.ToListAsync());
        }

        public async Task<IActionResult> details(int? id)
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeenusId,Info,Hind,Aeg")] Teenus teenus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teenus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teenus);
        }

        public IActionResult Teenus()
        {
            return View();
        }
    }
}
