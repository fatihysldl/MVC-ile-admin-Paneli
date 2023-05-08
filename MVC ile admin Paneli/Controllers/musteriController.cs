using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
using PagedList;
using PagedList.Mvc;
namespace MVC_ile_admin_Paneli.Controllers
{
   
    public class musteriController : Controller
    {
        adminEntities db = new adminEntities();
       
        // GET: mudteri
        public ActionResult Index(int sayfa=1)
        {

            var musteriler = db.tblMusteri.Where(x=>x.musteriDurum==true).ToList().ToPagedList(sayfa,5);
            return View(musteriler);
        }
        [HttpGet]
        public ActionResult musteriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult musteriEkle(tblMusteri p)
        {
            p.musteriDurum = true;
            db.tblMusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(int id)
        {
            var musteri = db.tblMusteri.Find(id);
            musteri.musteriDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult musteriGuncelleSayfasi(int id)
        {
               var musteri = db.tblMusteri.Find(id);
               return View("musteriGuncelleSayfasi",musteri);      
        }
        public ActionResult musteriGuncelle(tblMusteri p)
        {
            var musteri = db.tblMusteri.Find(p.musteriId);
            musteri.musteriAd = p.musteriAd;
            musteri.musterisSoyad = p.musterisSoyad;
            musteri.musteriSehir = p.musteriSehir;
            musteri.musteriBakiye = p.musteriBakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}