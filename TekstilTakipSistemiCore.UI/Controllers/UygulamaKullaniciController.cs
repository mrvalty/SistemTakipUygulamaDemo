using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DAL.Response;
using TekstilTakipSistemiCore.DATA.Context;
using TekstilTakipSistemiCore.UI.Models;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class UygulamaKullaniciController : Controller
    {
        private readonly ProjectContext _context;
        public UygulamaKullaniciController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult UygulamaKullaniciTanimi(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();
            return View("UygulamaKullaniciTanimi", "_Layout");
        }

        [HttpPost]
        public IActionResult UygulamaKullaniciTanimi(string txtkullaniciadi, string txtad1, string txtsoyad, string txtsifre)
        {
            if (String.IsNullOrEmpty(txtkullaniciadi))
            {
                TempData["Message"] = "Lütfen tüm alanları doldurunuz...";
                TempData["Sonuc"] = 0;
                return View();
            }

            UygulamaKullanici kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.KullaniciAdi == txtkullaniciadi && x.Ad == txtsoyad);
            if (kullanici == null)
            {
                UygulamaKullanici _kullanici = new UygulamaKullanici();
                _kullanici.KullaniciAdi = txtkullaniciadi;
                _kullanici.Ad = txtad1;
                _kullanici.Soyad = txtsoyad;
                _kullanici.Sifre = txtsifre;
                _kullanici.OlusturmaZamani = DateTime.Now;
                _kullanici.AktifMi = true;

                _context.UygulamaKullanicilar.Add(_kullanici);
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

      
    }
}
