using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
namespace MVC_ile_admin_Paneli.Controllers
{
    public class kategoriController : Controller
    {
        // GET: kategori
        adminEntities db = new adminEntities();
        
        public ActionResult Index(string p)
        {
            var kategoriler = db.tblKategori.ToList();
            if (!string.IsNullOrEmpty(p))
            {
                kategoriler = kategoriler.Where(x => (x.kategoriAd.StartsWith(p.ToUpper()))).ToList();
            }
            //var kategoriler = db.tblKategori.ToList();
            return View(kategoriler.ToList());
        }
        [HttpGet]
        public ActionResult kategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult kategoriEkle(tblKategori p)
        {
            db.tblKategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult kategoriGuncelleSayfasi(int id)
        {
            var ktgr = db.tblKategori.Find(id);
            return View("kategoriGuncelleSayfasi", ktgr);
        }
        [HttpPost]
        public ActionResult kategoriGuncelle(tblKategori p)
        {
            var ktgr = db.tblKategori.Find(p.kategoriId);
            ktgr.kategoriAd = p.kategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult kategoriSil(int id)
        {
            var ktgr = db.tblKategori.Find(id);
            db.tblKategori.Remove(ktgr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}