using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
using System.Web.Security;
namespace MVC_ile_admin_Paneli.Controllers
{
    [AllowAnonymous]
    public class girisYapController : Controller
    {
        // GET: girisYap
        adminEntities db = new adminEntities();
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult giris(tblAdminGiris p)
        {
            var girisBilgileri = db.tblAdminGiris.FirstOrDefault(x => x.kullaniciAdi == p.kullaniciAdi && x.adminSifre == p.adminSifre);
            if (girisBilgileri != null)
            {
                FormsAuthentication.SetAuthCookie(girisBilgileri.kullaniciAdi, false);
                return RedirectToAction("Index", "AnaSayfa");
            }
            else
            {
                ViewBag.hataliGiris="Yanlış Kullanıcı Adı Ya Da Şifre";
                return View("Index");
            }

        }
    }
}