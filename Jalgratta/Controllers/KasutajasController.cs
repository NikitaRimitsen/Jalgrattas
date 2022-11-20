using System;
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
        [Authorize(Policy = "readpolicy")]
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

                try
                {
                    SmtpMail oMail = new SmtpMail("TryIt");

                    // Your email address
                    oMail.From = "liveid@hotmail.com";

                    // Set recipient email address
                    oMail.To = "support@emailarchitect.net";

                    // Set email subject
                    oMail.Subject = "test email from hotmail, outlook, office 365 account";

                    // Set email body
                    oMail.TextBody = "this is a test email sent from c# project using hotmail.";

                    // Hotmail/Outlook SMTP server address
                    SmtpServer oServer = new SmtpServer("smtp.office365.com");

                    // If your account is office 365, please change to Office 365 SMTP server
                    // SmtpServer oServer = new SmtpServer("smtp.office365.com");

                    // User authentication should use your
                    // email address as the user name.
                    oServer.User = "liveid@hotmail.com";

                    // If you got authentication error, try to create an app password instead of your user password.
                    oServer.Password = "your password or app password";

                    // use 587 TLS port
                    oServer.Port = 587;

                    // detect SSL/TLS connection automatically
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    Console.WriteLine("start to send email over TLS...");

                    SmtpClient oSmtp = new SmtpClient();
                    oSmtp.SendMail(oServer, oMail);

                    Console.WriteLine("email was sent successfully!");
                }
                catch (Exception ep)
                {
                    Console.WriteLine("failed to send email with the following error:");
                    Console.WriteLine(ep.Message);
                }


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

        //public void E_mail(Kasutaja kasutaja)
        //{
        //    try
        //    {
        //        WebMail.SmtpServer = "smtp.yandex.com";
        //        WebMail.SmtpPort = 456;
        //        WebMail.EnableSsl = true;
        //        WebMail.UserName = "testtestov1chtest@yandex.com";
        //        WebMail.Password = "testtestovisch";//opi-ldUIK4I1
        //        WebMail.From = "testtestov1chtest@yandex.com";
        //        WebMail.Send("testtestov1chtest@yandex.com", "Vastus kutsele", " vastas ");
        //        ViewBag.Message = "Kiri on saatnud!";
        //    }
        //    catch (Exception)
        //    {
        //        ViewBag.Message = "Mul on kahjul! Ei saa kirja saada!!!";
        //    }
        //}

        //public void E_mail2()
        //{ 
        //    WebMail.SmtpServer = "imap.mail.ru";
        //    WebMail.SmtpPort = 465;
        //    WebMail.EnableSsl = true;
        //    WebMail.UserName = "testtestovich74@mail.ru";
        //    WebMail.Password = "opi-ldUIK4I1";
        //    WebMail.From = "testtestovich74@mail.ru";
        //    //WebMail.Send(komu, "Napominanie","ne zabud pridi");
        //    WebMail.Send("aleksei.tiora@gmail.com", "Meeldetuletus", "Ärge unustage tulla puhkusele :) ");

        //}

        //public async void E_mail3()
        //{
        //    MailAddress from = new MailAddress("testtestovich74@mail.ru", "Tom");
        //    MailAddress to = new MailAddress("aleksei.tiora@gmail.com");
        //    MailMessage m = new MailMessage(from, to);
        //    m.Subject = "Тест";
        //    m.Body = "Письмо-тест 2 работы smtp-клиента";
        //    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 465);
        //    smtp.Credentials = new NetworkCredential("testtestovich74@mail.ru", "opi-ldUIK4I1");
        //    smtp.EnableSsl = true;
        //    await smtp.SendMailAsync(m);
        //    Console.WriteLine("Письмо отправлено");
        //}
    }
}
