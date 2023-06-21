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
 
    public class KirlenmeSurecController : Controller
    {
        private readonly ProjectContext _context;
        public KirlenmeSurecController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult KirlenmeSureci(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();


            var kirliDepoList = (from depo in _context.KirliDepolari
                                 join kimlik in _context.Kimliklendirmeler on depo.KimliklendirmeId equals kimlik.Id
                                 join mefrusat in _context.Mefrusatlar on kimlik.MefrusatId equals mefrusat.Id
                                 join kisi in _context.UygulamaKullanicilar on depo.KullaniciId equals kisi.Id
                                 select new KirlenmeBilgileriListViewModel()
                                 {
                                     EtiketNo = kimlik.EtiketNo,
                                     MefrusatTur = mefrusat.MefrusatTuru,
                                     TartimTarihi = depo.TartimTarihi,
                                     HasarDurumu = depo.HasarDurumu,
                                     TeslimDurumu = depo.TeslimDurumu,
                                     KullanimdaMi = depo.KullanimdaMi,
                                     HurdaMi = depo.HurdaMi,
                                     Olusturan = kisi.Ad + " " + kisi.Soyad

                                 }).ToList();
            if (kirliDepoList != null)
            {
                ViewData["KirliListesi"] = kirliDepoList;
            }

            return View("KirlenmeSureci", "_Layout");
        }
        [HttpPost]
        [Route("KirlenmeSurec/MefrusatTurBilgiGetir")]
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
        public IActionResult KirlenmeBilgileriKaydet(KirlenmeBilgileriKaydetRequest request)
        {
            #region Kullanıcı Bilgisi Alma
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            UygulamaKullanici kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            #endregion
            KirlenmeBilgileriKaydetResponse response = new KirlenmeBilgileriKaydetResponse();
            Kimliklendirme _kimliklendirme = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo == request.EtiketNo && x.AktifMi == true);
            if (_kimliklendirme != null)
            {
                KirliDepo kirliDepo = new KirliDepo();
                Kimliklendirme kimlik = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo == request.EtiketNo && x.AktifMi == true);
                if (request.HurdaMi == true)
                {
                    kirliDepo.KimliklendirmeId = _kimliklendirme.Id;
                    kirliDepo.OlusturmaZamani = DateTime.Now;
                    kirliDepo.HasarDurumu = Enum.Parse<EHasarDurumu>(request.HasarDurumu);
                    kirliDepo.TeslimDurumu = Enum.Parse<ETeslimDurumu>(request.TeslimDurumu);
                    kirliDepo.TartimTarihi = request.TartimTarihi;
                    kirliDepo.KullanimdaMi = request.KullanimdaMi;
                    kirliDepo.HurdaMi = request.HurdaMi;
                    kirliDepo.KullaniciId = kullanici.Id;
                    kirliDepo.AktifMi = false;
                    kimlik.AktifMi = false;
                    kirliDepo.KirlenmeSayisi = 1;
                }
                else
                {
                    kirliDepo.KimliklendirmeId = _kimliklendirme.Id;
                    kirliDepo.OlusturmaZamani = DateTime.Now;
                    kirliDepo.HasarDurumu = Enum.Parse<EHasarDurumu>(request.HasarDurumu);
                    kirliDepo.TeslimDurumu = Enum.Parse<ETeslimDurumu>(request.TeslimDurumu);
                    kirliDepo.TartimTarihi = request.TartimTarihi;
                    kirliDepo.KullanimdaMi = request.KullanimdaMi;
                    kirliDepo.HurdaMi = request.HurdaMi;
                    kirliDepo.KullaniciId = kullanici.Id;
                    kirliDepo.AktifMi = true;
                    kimlik.AktifMi = true;
                    kirliDepo.KirlenmeSayisi = 1;
                }
                
                _context.KirliDepolari.Add(kirliDepo);
                _context.SaveChanges();

                TempData["Message"] = "Kirlenme süreci verileri kaydı başarılı.";
                TempData["Sonuc"] = 1;
                response.Sonuc = 1;
            }
            else
            {
                TempData["Message"] = "Etiket no bilgisi olmadan kayıt yapılamaz.";
                response.Sonuc = 0;
            }

            return Json(response);
        }

        public IActionResult KirlenmeBilgileriListele()
        {
            return View();
        }

    }
}
