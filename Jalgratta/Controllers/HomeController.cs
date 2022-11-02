using Jalgratta.Data;
using Jalgratta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Jalgratta.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        Teenus teenus;
        Kasutaja kasutaja;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Hooldus()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Rentimine()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Kasutaja()
        {
            return View();
        }
        public IActionResult Tootajad()
        {
            return View();
        }
        public IActionResult Teenusetelimus()
        {
            return View();
        }
        public IActionResult Registreemine()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Ankeet(Kasutaja kasutaja)
        {
            if (ModelState.IsValid)
            {
                db.Kasutaja.Add(kasutaja);
                db.SaveChanges();
                return View("Thanks", kasutaja);
            }
            else
            {
                return View();
            }
        }

    }
}
