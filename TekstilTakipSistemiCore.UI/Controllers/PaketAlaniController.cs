using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Entities;
using TekstilTakipSistemiCore.DAL.Models;
using TekstilTakipSistemiCore.DAL.Request;
using TekstilTakipSistemiCore.DAL.Response;
using TekstilTakipSistemiCore.DATA.Context;
using TekstilTakipSistemiCore.UI.Models;
using TekstilTakipSistemiCore.UI.Utils;

namespace TekstilTakipSistemiCore.UI.Controllers
{
    public class PaketAlaniController : Controller
    {
        private readonly ProjectContext _context;
        public PaketAlaniController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult PaketAlaniEkle(UygulamaKullanici kullanici, int id)
        {
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();


            var paketlistesi = (from p in _context.PaketAlanlari
                                join m in _context.Mefrusatlar on p.MefrusatId equals m.Id
                                join k in _context.Kimliklendirmeler on p.KimliklendirmeId equals k.Id
                                where p.AktifMi == true
                                select new PaketAlaniListViewModel()
                                {
                                    PaketNo = p.PaketNo

                                }).Distinct().ToList();
            if (paketlistesi != null)
            {
                ViewData["PaketListesi"] = paketlistesi;
            }


            return View("PaketAlaniEkle", "_Layout");
        }
        [HttpPost]
        public JsonResult PaketAlaniEkle(PaketOlusturRequest request)
        {
            #region Kullanıcı Bilgisi Alma
            int id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            UygulamaKullanici kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            #endregion
            PaketOlusturResponse response = new PaketOlusturResponse();
            

            if (String.IsNullOrEmpty(request.odano))
            {
                TempData["Message"] = "Lütfen oda numarasını boş geçmeyin...";
                TempData["Sonuc"] = 0;

                response.sonuc = 2;
               
            }
            else
            {
                var dataidbol = request.dataid.Split(',');
                RandomString randomString = new RandomString();
                var paketno = randomString.RandomStringValue(5); ;
                for (int i = 0; i < dataidbol.Length - 1; i++)
                {
                    var index = dataidbol[i];

                    Kimliklendirme kimliklendirme = _context.Kimliklendirmeler.FirstOrDefault(x => x.Id == Convert.ToInt32(index) && x.AktifMi == true);
                    PaketAlani paketAlani = new PaketAlani();

                    if (kimliklendirme != null)
                    {
                        paketAlani.AktifMi = true;
                        paketAlani.MefrusatId = kimliklendirme.MefrusatId;
                        paketAlani.KimliklendirmeId = kimliklendirme.Id;
                        paketAlani.OlusturmaZamani = DateTime.Now;
                        paketAlani.PaketNo = paketno;
                        paketAlani.OdaNumarasi = request.odano;
                        paketAlani.KullaniciId = kullanici.Id;

                        _context.PaketAlanlari.Add(paketAlani);
                        _context.SaveChanges();

                        TempData["Message"] = "Paketleme İşlemi Başarılı";
                        TempData["Sonuc"] = 1;

                        response.sonuc = 1;

                    }
                    else
                    {
                        TempData["Message"] = "Mefruşat türünün kimliklendirme işlemi bulunmamaktadır.";
                        TempData["Sonuc"] = 0;
                        response.sonuc = 0;

                    }
                }
            }
            

            return Json(response);
            
        }

        [HttpPost]
        public JsonResult EtiketNoGoreTabloyaEkle(string etiketno)
        {
            EtiketNoGoreTabloyaEkleResponse response = new EtiketNoGoreTabloyaEkleResponse();
            Kimliklendirme _kimlik = _context.Kimliklendirmeler.FirstOrDefault(x => x.EtiketNo.Equals(etiketno) && x.AktifMi == true);
            if (_kimlik == null)
            {

                response.zSonuc = 0;
            }
            else
            {
                Mefrusat mefrusat = _context.Mefrusatlar.FirstOrDefault(x => x.AktifMi && x.Id == _kimlik.MefrusatId);

                if (mefrusat != null)
                {
                    var query = (from k in _context.Kimliklendirmeler
                                 join m in _context.Mefrusatlar on k.MefrusatId equals m.Id
                                 where k.AktifMi == true && k.EtiketNo == etiketno
                                 select new PaketAlaniSecimListViewModel()
                                 {
                                     rownum = k.Id,
                                     EtiketNo = k.EtiketNo,
                                     MefrusatTuru = m.MefrusatTuru
                                 }).ToList();
                    if (query.Count != 0)
                    {
                        response.zDizi = query;
                    }
                }
                response.zSonuc = 1;
            }
            return Json(response);
        }
        public IActionResult PaketAlaniListele(UygulamaKullanici kullanici, int id)
        {
            
            id = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.Id == id);
            ViewBag.Name = kullanici.Ad.ToUpper();
            ViewBag.Surname = kullanici.Soyad.ToUpper();

            var paketlistesi = (from p in _context.PaketAlanlari
                         join m in _context.Mefrusatlar on p.MefrusatId equals m.Id
                         join k in _context.Kimliklendirmeler on p.KimliklendirmeId equals k.Id
                         where p.AktifMi == true
                         select new PaketAlaniListViewModel()
                         {
                             PaketNo=p.PaketNo
                             
                         }).Distinct().ToList();
            if (paketlistesi !=null)
            {
                ViewData["PaketListesi"] = paketlistesi;
            }

            return View("PaketAlaniListele", "_Layout");

        }
        [HttpPost]
        public JsonResult PaketBilgileriDetay(PaketBilgileriDetayRequest request)
        {
            PaketDetayResponse response = new PaketDetayResponse();

            var paketdetay = (from paketalani in _context.PaketAlanlari
                              join kimlik in _context.Kimliklendirmeler on paketalani.KimliklendirmeId equals kimlik.Id
                              join mefrusat in _context.Mefrusatlar on kimlik.MefrusatId equals mefrusat.Id
                              join kisi in _context.UygulamaKullanicilar on paketalani.KullaniciId equals kisi.Id
                              where paketalani.PaketNo == request.paketNo.ToString() && paketalani.AktifMi == true
                              select new PaketAlaniSecimListViewModel()
                              {
                                  PaketNo = paketalani.PaketNo,
                                  EtiketNo = kimlik.EtiketNo,
                                  MefrusatTuru = mefrusat.MefrusatTuru,
                                  OdaNo = paketalani.OdaNumarasi,
                                  Olusturan= kisi.Ad +" "+kisi.Soyad,
                                  OlusturmaZamani=paketalani.OlusturmaZamani

                              }).ToList(); 

            if(paketdetay != null)
            {
                response.paketlist = paketdetay;
                response.sonuc = 1;
            }

            return Json(response);
        }

    }
}
