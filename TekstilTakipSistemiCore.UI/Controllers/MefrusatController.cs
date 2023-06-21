using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DATA.Context;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class MefrusatController : Controller
    {
        private readonly ProjectContext _context;
        public MefrusatController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult MefrusatTanimi(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();
            return View("MefrusatTanimi", "_Layout");
        }

        [HttpPost]
        public IActionResult MefrusatTanimi(string mefrusattur, string mefrusatkod)
        {
            if (String.IsNullOrEmpty(mefrusattur) || String.IsNullOrEmpty(mefrusatkod))
            {
                TempData["Message"] = "Lütfen tüm alanları doldurunuz...";
                TempData["Sonuc"] = 0;
                return View();
            }

            Mefrusat mefrusat = _context.Mefrusatlar.FirstOrDefault(x => x.MefrusatTuru == mefrusattur && x.MefrusatKodu == mefrusatkod);
            if (mefrusat == null)
            {
                Mefrusat _mefrusat = new Mefrusat();
                _mefrusat.MefrusatTuru = mefrusattur;
                _mefrusat.MefrusatKodu = mefrusatkod;
                _mefrusat.OlusturmaZamani = DateTime.Now;
                _mefrusat.AktifMi = true;

                _context.Mefrusatlar.Add(_mefrusat);
                _context.SaveChanges();

                TempData["Message"] = "Kayıt Başarılı";
                TempData["Sonuc"] = 1;
            }
            else
            {
                TempData["Message"] = "Sistemde daha önce kayıtlı.";
                TempData["Sonuc"] = 0;
            }

            return View();
        }
       
        public IActionResult MefrusatListele(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();


            List<Mefrusat> mefrusat = _context.Mefrusatlar.Where(x => x.AktifMi == true).ToList();
            if (mefrusat != null)
            {
                ViewData["Mefrusatlar"] = mefrusat;
            }

            return View("MefrusatListele", "_Layout");

        }
    }
}
