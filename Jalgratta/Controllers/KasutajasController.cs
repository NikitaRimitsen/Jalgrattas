﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta.Models;
using System.Web.Helpers;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using EASendMail;
using SmtpClient = EASendMail.SmtpClient;

namespace Jalgratta.Controllers
{
    public class KasutajasController : Controller
    {
        private readonly DataBase _context;

        public KasutajasController(DataBase context)
        {
            _context = context;
        }

        // GET: Kasutajas
        [Authorize(Policy = "writepolicy")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Kasutaja.ToListAsync());
        }

        // GET: Kasutajas/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Kasutajas/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Kasutajas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KasutajaId,Nimi,Perekonnanimi,Email,Vanus,telnumber")] Kasutaja kasutaja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kasutaja);
                await _context.SaveChangesAsync();
                E_mail(kasutaja);
                return RedirectToAction(nameof(Index));
            }

            return View(kasutaja);
            
        }
        
        // GET: Kasutajas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kasutaja == null)
            {
                return NotFound();
            }

            var kasutaja = await _context.Kasutaja.FindAsync(id);
            if (kasutaja == null)
            {
                return NotFound();
            }
            return View(kasutaja);
        }

        // POST: Kasutajas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KasutajaId,Nimi,Perekonnanimi,Email,Vanus,telnumber")] Kasutaja kasutaja)
        {
            if (id != kasutaja.KasutajaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kasutaja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KasutajaExists(kasutaja.KasutajaId))
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
            return View(kasutaja);
        }

        // GET: Kasutajas/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Kasutajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kasutaja == null)
            {
                return Problem("Entity set 'DataBase.Kasutaja'  is null.");
            }
            var kasutaja = await _context.Kasutaja.FindAsync(id);
            if (kasutaja != null)
            {
                _context.Kasutaja.Remove(kasutaja);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KasutajaExists(int id)
        {
          return _context.Kasutaja.Any(e => e.KasutajaId == id);
        }

        public async void E_mail(Kasutaja kasutaja)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = "jalgratta@hotmail.com";
                oMail.To = kasutaja.Email;
                oMail.Subject = "Kiri";
                oMail.TextBody = "Aitäh! Mis on meiega registreerunud!";
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
