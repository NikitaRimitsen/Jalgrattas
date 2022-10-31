using Jalgratta.Data;
using Jalgratta.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace Jalgratta.Controllers
{
    public class HomeController : Controller
    {
        Teenus teenus;
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

        //[HttpGet]
        //public IActionResult Registreemine()
        //{
        //    return View();
        //}

        //public ViewResult Registreemine(Teenus teenuse)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _contex.tenus.Teenus.Add(teenuse);
        //        _context.SaveChanges();
        //        return View("Thanks", teenuse);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        ApplicationDbContext db;


        [HttpGet]

        public ActionResult CreateKasutaja()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
    }
}