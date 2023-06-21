using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DATA.Context;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class KirliDetayRaporController : Controller
    {
        private readonly ProjectContext _context;

        public KirliDetayRaporController(ProjectContext context)
        {
            _context = context;
        }

        public IActionResult KirliDetayRaporAlani(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();
            return View("KirliDetayRaporAlani", "_Layout");
        }
    }
}
