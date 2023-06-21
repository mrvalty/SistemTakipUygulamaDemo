using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DATA.Context;
using TekstilTakipSistemiCore.UI.Models;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjectContext _context;
        

        public HomeController(ILogger<HomeController> logger, ProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(UygulamaKullanici kullanici, int id)
        {
            id =Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();
            return View("Index","_Layout");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
