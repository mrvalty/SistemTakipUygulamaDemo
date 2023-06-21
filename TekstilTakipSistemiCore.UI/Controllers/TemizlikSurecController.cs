using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DAL.Enums;
using TekstilTakipSistemiCore.DAL.Request;
using TekstilTakipSistemiCore.DAL.Response;
using TekstilTakipSistemiCore.DATA.Context;
using TekstilTakipSistemiCore.UI.Models;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class TemizlikSurecController : Controller
    {
        private readonly ProjectContext _context;
        public TemizlikSurecController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult TemizSureci(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();


            var temizDepoList = (from depo in _context.TemizDepolari
                                 join kimlik in _context.Kimliklendirmeler on depo.KimliklendirmeId equals kimlik.Id
                                 join mefrusat in _context.Mefrusatlar on kimlik.MefrusatId equals mefrusat.Id
                                 where depo.AktifMi == true
                                 select new TemizlikBilgileriListViewModel()
                                 {
                                     EtiketNo = kimlik.EtiketNo,
                                     MefrusatTur = mefrusat.MefrusatTuru,
                                     TartimTarihi = depo.TartimTarihi,
                                     YıkamaDurumu = depo.YikamaDurumu,
                                     TeslimDurumu = depo.TeslimDurumu,
                                     AmbalajlandiMi = depo.AmbalajlandiMi,
                                     BohcalandiMi = depo.BohcalandiMi

                                 }).ToList();
            if (temizDepoList != null)
            {
                ViewData["TemizListesi"] = temizDepoList;
            }

            return View("TemizSureci", "_Layout");
        }
        [HttpPost]
        [Route("TemizlikSurec/MefrusatTurBilgiGetir")]
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
        public IActionResult TemizlikBilgileriKaydet(TemizlikBilgileriKaydetRequest request)
        {
            #region Kullanıcı Bilgisi Alma
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            UygulamaKullanici kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            #endregion

            TemizlikBilgileriKaydetResponse response = new TemizlikBilgileriKaydetResponse();
            Kimliklendirme _kimliklendirme = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo == request.EtiketNo && x.AktifMi == true);
            if (_kimliklendirme != null)
            {
                TemizDepo temizDepo = new TemizDepo();
                temizDepo.KimliklendirmeId = _kimliklendirme.Id;
                temizDepo.OlusturmaZamani = DateTime.Now;
                temizDepo.YikamaDurumu = Enum.Parse<EYikamaDurumu>(request.YikamaDurumu);
                temizDepo.TeslimDurumu = Enum.Parse<ETeslimDurumu>(request.TeslimDurumu);
                temizDepo.TartimTarihi = request.TartimTarihi;
                temizDepo.BohcalandiMi = request.BohcalandiMi;
                temizDepo.AmbalajlandiMi = request.AmbalanlandiMi;
                temizDepo.AktifMi = true;
                temizDepo.YikanmaSayisi = 1;
                temizDepo.KullaniciId = kullanici.Id;

                _context.TemizDepolari.Add(temizDepo);
                _context.SaveChanges();

                TempData["Message"] = "Temizlik süreci verileri kaydı başarılı.";
                TempData["Sonuc"] = 1;
                response.sonuc = 1;
            }
            else
            {
                TempData["Message"] = "Etiket no bilgisi olmadan kayıt yapılamaz.";
                response.sonuc = 0;
            }

            return Json(response);
        }

        public IActionResult TemizlikBilgileriListele()
        {

            return View();
        }

    }
}
