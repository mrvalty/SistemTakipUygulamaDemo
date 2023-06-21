using Microsoft.AspNetCore.Authorization;
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
    public class LoginController : Controller
    {
        private readonly ProjectContext _context;
        public LoginController(ProjectContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string kullaniciAdi, string sifre)
        {

            UygulamaKullanici kullanici = _context.UygulamaKullanicilar.FirstOrDefault(x => x.KullaniciAdi == kullaniciAdi && x.Sifre == sifre);
            if (kullanici != null)
            {
                HttpContext.Session.SetInt32("id", kullanici.Id);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");

            return RedirectToAction("Login", "Login");

        }

    }
}
