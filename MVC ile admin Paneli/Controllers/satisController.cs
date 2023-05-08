using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
using System.Data.Entity;
namespace MVC_ile_admin_Paneli.Controllers
{
    public class satisController : Controller
    {
        // GET: satis
        adminEntities db = new adminEntities();
      
        public ActionResult Index()
        {
            var satislar = db.tblSatis.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult SatisSayfasi()
        {
            List<SelectListItem> ürün = (from x in db.tblUrunler.ToList()
                                         where (x.urunDurumu == true)
                                         select new SelectListItem
                                         {
                                             Text = x.urunAd,
                                             Value = x.urunId.ToString()
                                         }).ToList();
            ViewBag.urun = ürün;
            List<SelectListItem> personel = (from x in db.tblPersonel.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.personelAd + " " + x.personelSoyad,
                                                 Value = x.personelId.ToString()
                                             }).ToList();
            ViewBag.personeller = personel;
            List<SelectListItem> musteri = (from x in db.tblMusteri.ToList()
                                            where (x.musteriDurum == true)
                                            select new SelectListItem
                                            {
                                                Text = x.musteriAd + " " + x.musterisSoyad,
                                                Value = x.musteriId.ToString()
                                            }).ToList();
            ViewBag.musteriler = musteri;
            return View();
        }
        [HttpPost]
        public ActionResult SatisSayfasi(tblSatis p)
        {
            var urun = db.tblUrunler.Where(x => x.urunId == p.tblUrunler.urunId).FirstOrDefault();
            var personel = db.tblPersonel.Where(x => x.personelId == p.tblPersonel.personelId).FirstOrDefault();
            var musteri = db.tblMusteri.Where(x => x.musteriId == p.tblMusteri.musteriId).FirstOrDefault();
            p.tblUrunler = urun;
            p.tblPersonel = personel;
            p.tblMusteri =musteri;
            urun.kalanStok -= 1;
            db.Entry(urun).State = EntityState.Modified;
            musteri.musteriBakiye -= p.tblUrunler.urunSatisFiyat;
            db.Entry(musteri).State = EntityState.Modified;
            db.tblSatis.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}