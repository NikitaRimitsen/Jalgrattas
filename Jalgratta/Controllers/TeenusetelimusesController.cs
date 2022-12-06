using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta.Models;
using EASendMail;
using Microsoft.AspNetCore.Authorization;

namespace Jalgratta.Controllers
{
    public class TeenusetelimusesController : Controller
    {
        private readonly DataBase _context;

        public TeenusetelimusesController(DataBase context)
        {
            _context = context;
        }

        // GET: Teenusetelimuses
        public async Task<IActionResult> Index()
        {
            var dataBase = _context.Teenusetelimuse.Include(t => t.Teenus).Include(t => t.Tootajad);
            return View(await dataBase.ToListAsync());
        }

        // GET: Teenusetelimuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teenusetelimuse == null)
            {
                return NotFound();
            }

            var teenusetelimuse = await _context.Teenusetelimuse
                .Include(t => t.Teenus)
                .Include(t => t.Tootajad)
                .FirstOrDefaultAsync(m => m.TelimusId == id);
            if (teenusetelimuse == null)
            {
                return NotFound();
            }

            return View(teenusetelimuse);
        }

        // GET: Teenusetelimuses/Create
        [Authorize(Policy = "readpolicy")]
        public IActionResult Create()
        {
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info");
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi");
            return View();
        }

        // POST: Teenusetelimuses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TelimusId,Nimi,Perekonnanimi,telnumber,TeenusId,TootajadId,Kuupaev")] Teenusetelimuse teenusetelimuse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teenusetelimuse);
                await _context.SaveChangesAsync();
                E_mail(teenusetelimuse);
                return RedirectToAction("Index", "Home");
            }
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimuse.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimuse.TootajadId);
            return View(teenusetelimuse);
        }

        // GET: Teenusetelimuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teenusetelimuse == null)
            {
                return NotFound();
            }

            var teenusetelimuse = await _context.Teenusetelimuse.FindAsync(id);
            if (teenusetelimuse == null)
            {
                return NotFound();
            }
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimuse.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimuse.TootajadId);
            return View(teenusetelimuse);
        }

        // POST: Teenusetelimuses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TelimusId,Nimi,Perekonnanimi,telnumber,TeenusId,TootajadId,Kuupaev")] Teenusetelimuse teenusetelimuse)
        {
            if (id != teenusetelimuse.TelimusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teenusetelimuse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeenusetelimuseExists(teenusetelimuse.TelimusId))
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
            ViewData["TeenusId"] = new SelectList(_context.Teenus, "TeenusId", "Info", teenusetelimuse.TeenusId);
            ViewData["TootajadId"] = new SelectList(_context.Tootajad, "TootajadId", "Nimi", teenusetelimuse.TootajadId);
            return View(teenusetelimuse);
        }

        // GET: Teenusetelimuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teenusetelimuse == null)
            {
                return NotFound();
            }

            var teenusetelimuse = await _context.Teenusetelimuse
                .Include(t => t.Teenus)
                .Include(t => t.Tootajad)
                .FirstOrDefaultAsync(m => m.TelimusId == id);
            if (teenusetelimuse == null)
            {
                return NotFound();
            }

            return View(teenusetelimuse);
        }

        // POST: Teenusetelimuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teenusetelimuse == null)
            {
                return Problem("Entity set 'DataBase.Teenusetelimuse'  is null.");
            }
            var teenusetelimuse = await _context.Teenusetelimuse.FindAsync(id);
            if (teenusetelimuse != null)
            {
                _context.Teenusetelimuse.Remove(teenusetelimuse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeenusetelimuseExists(int id)
        {
          return _context.Teenusetelimuse.Any(e => e.TelimusId == id);
        }

        public async void E_mail(Teenusetelimuse teenusetelimuse)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = "jalgratta@hotmail.com";

                oMail.To = User.Identity?.Name.ToString(); ;
                oMail.Subject = "Broneerimine";
                oMail.TextBody = $"Aitäh! {teenusetelimuse.Nimi} et broneerisite meie juures jalgratta hoolduse!\nTeenuse kuupäev: {teenusetelimuse.Kuupaev}";
                //oMail.Date = "teenusetelimuskasutaja.Kuupaev;
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
