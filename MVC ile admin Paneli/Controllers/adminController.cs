using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
namespace MVC_ile_admin_Paneli.Controllers
{
    public class adminController : Controller
    {
        adminEntities db = new adminEntities();
        
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult adminEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminEkle(tblAdminGiris p)
        {
            if (ModelState.IsValid)
            {
                var admin = db.tblAdminGiris.FirstOrDefault(a => a.kullaniciAdi == p.kullaniciAdi);
                if (admin != null)
                {
                    ModelState.AddModelError("kullaniciAdi", "Bu kullanıcı adı zaten kullanılıyor.");
                    return View("Index");
                }
                db.tblAdminGiris.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}