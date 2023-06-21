using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DAL.Response;
using TekstilTakipSistemiCore.DATA.Context;
using TekstilTakipSistemiCore.DAL.Models;
using TekstilTakipSistemiCore.DAL.Enums;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class TemizlikDetayRaporController : Controller
    {
        private readonly ProjectContext _context;
        public TemizlikDetayRaporController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult TenizlikDetayRaporAlani(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();
            return View("TemizlikDetayRapor", "_Layout");
        }

        public IActionResult TemizlikDetayRaporuListele(int mefrusatlist)
        {
            TemizlikDetayRaporuResponse response = new TemizlikDetayRaporuResponse();
            Mefrusat _mefrusat = _context.Mefrusatlar.FirstOrDefault(x => x.AktifMi && x.Id == mefrusatlist);

            if (_mefrusat != null)
            {
                var query = (from k in _context.Kimliklendirmeler
                             join m in _context.Mefrusatlar on k.MefrusatId equals m.Id
                             join t in _context.TemizDepolari on k.Id equals t.KimliklendirmeId
                             join kd in _context.KirliDepolari on k.Id equals kd.KimliklendirmeId
                             where k.AktifMi == true && m.Id == mefrusatlist
                             select new
                             {
                                 k.EtiketNo,
                                 m.MefrusatTuru,
                                 t.YikamaDurumu,
                                 t.TeslimDurumu,
                                 t.YikanmaSayisi,
                                 kd.KirlenmeSayisi
                             }).GroupBy(x => new { x.EtiketNo, x.MefrusatTuru, x.YikamaDurumu, x.TeslimDurumu }).Select(x => new TemizlikDetayRaporListViewModel
                             {
                                 EtiketNo = x.Key.EtiketNo,
                                 MefrusatTur = x.Key.MefrusatTuru,
                                 YikamaDurumu = x.Key.YikamaDurumu,
                                 TeslimDurumu = x.Key.TeslimDurumu,
                                 YikanmaSayisi = x.Sum(n => n.YikanmaSayisi),
                                 KirlenmeSayisi = x.Sum(m => m.KirlenmeSayisi)
                             }).ToList();

                if (query != null)
                {
                    response.zDizi = query;
                }
                response.zSonuc = 1;
            }

            return Json(response);

        }




        [HttpPost]
        [Route("TemizlikDetayRapor/MefrusatTurBilgiGetir")]
        public IActionResult MefrusatTurBilgiGetir(string etiketno)
        {
            EtiketNoMefrusatGetirResponse response = new EtiketNoMefrusatGetirResponse();
            Kimliklendirme _kimlik = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo.Equals(etiketno) && x.AktifMi == true);
            Mefrusat mefrusat = _context.Mefrusatlar.FirstOrDefault(x => x.AktifMi && x.Id == _kimlik.MefrusatId);
            if (_kimlik != null)
            {
                if (mefrusat != null)
                {
                    response.MefrusatTur = mefrusat.MefrusatTuru.ToUpperInvariant();
                }
            }
            else
            {
                TempData["Message"] = "Bu kimlik no ya ait kayıt bulunamadı.";
                TempData["Sonuc"] = 0;

            }


            return Json(response);

        }
        [HttpPost]

        public IActionResult TemizlikBilgileriListele()
        {



            return View();
        }

    }
}
