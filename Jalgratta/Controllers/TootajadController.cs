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
    public class TootajadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TootajadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get: teenus
        public async Task<IActionResult> index()
        {
            return View(await _context.Tootajad.ToListAsync());
        }

        public async Task<IActionResult> details(int? id)
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
    }
}
