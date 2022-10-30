using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jalgratta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jalgratta.Controllers
{
    public class KasutajaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KasutajaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> index()
        {
            return View(await _context.Kasutaja.ToListAsync());
        }

        public async Task<IActionResult> details(int? id)
        {
            if (id == null || _context.Kasutaja == null)
            {
                return NotFound();
            }

            var kasutaja = await _context.Kasutaja
                .FirstOrDefaultAsync(m => m.KasutajaId == id);
            if (kasutaja == null)
            {
                return NotFound();
            }

            return View(kasutaja);
        }
    }
}
