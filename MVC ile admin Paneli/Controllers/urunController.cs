using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
namespace MVC_ile_admin_Paneli.Controllers
{
    public class urunController : Controller
    {
        // GET: urun
        adminEntities db = new adminEntities();
        
        public ActionResult Index(string p)
        {
            var urunler = db.tblUrunler.Where(x => x.urunDurumu == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => (x.urunAd.StartsWith(p.ToUpper()) ) && x.urunDurumu == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult urunEkle()
        {
            List<SelectListItem> ktgr = (from x in db.tblKategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.kategoriAd,
                                             Value = x.kategoriId.ToString()
                                         }).ToList();
            ViewBag.kategoriler = ktgr;
            return View();
        }
        [HttpPost]
        public ActionResult urunEkle(tblUrunler p)
        {
            var ktgr = db.tblKategori.Where(x => x.kategoriId == p.urunKategori).FirstOrDefault();
            p.tblKategori = ktgr;
            p.urunDurumu = true;
            db.tblUrunler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGuncelleSayfasi(int id)
        {
            List<SelectListItem> kategori = (from x in db.tblKategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.kategoriAd,
                                                 Value = x.kategoriId.ToString()
                                             }).ToList();
            var ktgr = db.tblUrunler.Find(id);
            ViewBag.kategori = kategori;

            return View("UrunGuncelleSayfasi",ktgr);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(tblUrunler p)
        {
            var urunId = db.tblUrunler.Find(p.urunId);
            urunId.urunAd = p.urunAd;
            urunId.urunMarka = p.urunMarka;
            urunId.toplamStok = p.toplamStok;
            urunId.kalanStok = p.kalanStok;
            urunId.urunAlisFiyat = p.urunSatisFiyat;
            var kategori = db.tblKategori.Where(x => x.kategoriId == p.urunId).FirstOrDefault();
            urunId.urunKategori = kategori.kategoriId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urunId = db.tblUrunler.Find(id);
            urunId.urunDurumu = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}