using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta.Models;
using EASendMail;
using SmtpClient = EASendMail.SmtpClient;

namespace Jalgratta.Controllers
{
    public class TeenusetelimuskasutajasController : Controller
    {
        private readonly DataBase _context;

        public TeenusetelimuskasutajasController(DataBase context)
        {
            _context = context;
        }

        // GET: Teenusetelimuskasutajas
        public async Task<IActionResult> Index()
        {
            var dataBase = _context.Teenusetelimuskasutaja.Include(t => t.Teenus).Include(t => t.Tootajad);
            return View(await dataBase.ToListAsync());
        }

        // GET: Teenusetelimuskasutajas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teenusetelimuskasutaja == null)
            {
                return NotFound();
            }

            var teenusetelimuskasutaja = await _context.Teenusetelimuskasutaja
                .Include(t => t.Teenus)
                .Include(t => t.Tootajad)
                .FirstOrDefaultAsync(m => m.TelimusId == id);
            if (teenusetelimuskasutaja == null)
            {
                return NotFound();
            }

            return View(teenusetelimuskasutaja);
        }

        // GET: Teenusetelimuskasutajas/Create
        public IActionResult Create()
        {
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info");
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi");
            return View();
        }

        // POST: Teenusetelimuskasutajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TelimusId,Nimi,Perekonnanimi,Email,Vanus,telnumber,TootajadId,TeenusId,Kuupaev")] Teenusetelimuskasutaja teenusetelimuskasutaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teenusetelimuskasutaja);
                await _context.SaveChangesAsync();
                E_mail(teenusetelimuskasutaja);
                return RedirectToAction("Index", "Home");
            }
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimuskasutaja.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimuskasutaja.TootajadId);
            return View(teenusetelimuskasutaja);
        }

        // GET: Teenusetelimuskasutajas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teenusetelimuskasutaja == null)
            {
                return NotFound();
            }

            var teenusetelimuskasutaja = await _context.Teenusetelimuskasutaja.FindAsync(id);
            if (teenusetelimuskasutaja == null)
            {
                return NotFound();
            }
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimuskasutaja.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimuskasutaja.TootajadId);
            return View(teenusetelimuskasutaja);
        }

        // POST: Teenusetelimuskasutajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TelimusId,Nimi,Perekonnanimi,Email,Vanus,telnumber,TootajadId,TeenusId,Kuupaev")] Teenusetelimuskasutaja teenusetelimuskasutaja)
        {
            if (id != teenusetelimuskasutaja.TelimusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teenusetelimuskasutaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeenusetelimuskasutajaExists(teenusetelimuskasutaja.TelimusId))
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
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimuskasutaja.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimuskasutaja.TootajadId);
            return View(teenusetelimuskasutaja);
        }

        // GET: Teenusetelimuskasutajas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teenusetelimuskasutaja == null)
            {
                return NotFound();
            }

            var teenusetelimuskasutaja = await _context.Teenusetelimuskasutaja
                .Include(t => t.Teenus)
                .Include(t => t.Tootajad)
                .FirstOrDefaultAsync(m => m.TelimusId == id);
            if (teenusetelimuskasutaja == null)
            {
                return NotFound();
            }

            return View(teenusetelimuskasutaja);
        }

        // POST: Teenusetelimuskasutajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teenusetelimuskasutaja == null)
            {
                return Problem("Entity set 'DataBase.Teenusetelimuskasutaja'  is null.");
            }
            var teenusetelimuskasutaja = await _context.Teenusetelimuskasutaja.FindAsync(id);
            if (teenusetelimuskasutaja != null)
            {
                _context.Teenusetelimuskasutaja.Remove(teenusetelimuskasutaja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeenusetelimuskasutajaExists(int id)
        {
          return _context.Teenusetelimuskasutaja.Any(e => e.TelimusId == id);
        }

        public async void E_mail(Teenusetelimuskasutaja teenusetelimuskasutaja)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = "jalgratta@hotmail.com";

                oMail.To = teenusetelimuskasutaja.Email;
                oMail.Subject = "Broneerimine";
                oMail.TextBody = $"Aitäh!, {teenusetelimuskasutaja.Nimi} et broneerisite meie juures jalgratta hoolduse!\nTeenuse kuupäev: {teenusetelimuskasutaja.Kuupaev}";
                oMail.Date = teenusetelimuskasutaja.Kuupaev;
                SmtpServer oServer = new SmtpServer("smtp.office365.com");
                oServer.User = "jalgratta@hotmail.com";
                oServer.Password = "jalgratajarent4";
                oServer.Port = 587;
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                //Console.WriteLine("start to send email over TLS...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                //Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                //Console.WriteLine("failed to send email with the following error:");
                //Console.WriteLine(ep.Message);
            }
        }
    }
}
