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
    public class KimliklendirmeController : Controller
    {
        private readonly ProjectContext _context;
        public KimliklendirmeController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult KimliklendirmeTanimi(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();
            return View("KimliklendirmeTanimi", "_Layout");
        }

        [HttpPost]
        public IActionResult KimliklendirmeTanimi(int mefrusatlist, string hasarAciklama, string etiketno)
        {
            if (String.IsNullOrEmpty(etiketno) || mefrusatlist == -1)
            {
                TempData["Message"] = "Lütfen tüm alanları doldurunuz...";
                TempData["Sonuc"] = 0;
                return View();
            }
            Kimliklendirme etiketkontrol = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo == etiketno && x.AktifMi == true);
            if (etiketkontrol != null)
            {
                TempData["Message"] = "Bu etiket numarasına ait kimliklendirme işlemi daha önce yapılmıştır.";
                TempData["Sonuc"] = 0;
            }
            else
            {
                Kimliklendirme kimlik = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo == etiketno && x.AktifMi == true && x.MefrusatId == mefrusatlist);

                if (kimlik == null)
                {
                    Kimliklendirme _kimlik = new Kimliklendirme();
                    _kimlik.MefrusatId = mefrusatlist;
                    _kimlik.HasarAciklama = hasarAciklama;
                    _kimlik.EtiketNo = etiketno;
                    _kimlik.OlusturmaZamani = DateTime.Now;
                    _kimlik.AktifMi = true;

                    _context.Kimliklendirmeler.Add(_kimlik);
                    _context.SaveChanges();

                    TempData["Message"] = "Kayıt Başarılı";
                    TempData["Sonuc"] = 1;
                }
            }

            return View();
        }

        public IActionResult KimliklendirmeListele(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();

            //List<KimliklendirmeListViewModel> kimliklist = new List<KimliklendirmeListViewModel>();
            var kimlik = (from a in _context.Kimliklendirmeler
                          join b in _context.Mefrusatlar on a.MefrusatId equals b.Id
                          where a.AktifMi == true
                          select new KimliklendirmeListViewModel
                          {
                              EtiketNo = a.EtiketNo,
                              HasarAciklama = a.HasarAciklama,
                              MefrusatTur = b.MefrusatTuru,

                          }).ToList();

            if (kimlik != null)
            {
                ViewData["Kimliklendirmeler"] = kimlik;
            }

            return View("KimliklendirmeListele", "_Layout");


        }
        [HttpPost]
        public IActionResult KimliklendirmeMefrusatListele()
        {
            List<MefrusatListViewModel> mefrusatList = new List<MefrusatListViewModel>();
            var query = (from mef in _context.Mefrusatlar
                         where mef.AktifMi == true
                         select new MefrusatListViewModel()
                         {
                             MefrusatId = mef.Id,
                             MefrusatTuru = mef.MefrusatTuru
                         }).ToList();
            if (query.Count != 0)
            {
                mefrusatList.AddRange(query);
            }
            return Json(mefrusatList);

        }
    }
}
