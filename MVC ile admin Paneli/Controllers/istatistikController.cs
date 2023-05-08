using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ile_admin_Paneli.Models.entity;
namespace MVC_ile_admin_Paneli.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        adminEntities db = new adminEntities();
        public ActionResult Index()
        {
            //Ürünler tablosundaki iphone stoğunun istatisiği
            var iphoneStok = db.tblUrunler
                .Where(u => u.urunAd == "iphone")
                .Select(u => new {
                    ToplamStok = u.toplamStok,
                    KalanStok = u.kalanStok
                })
                .FirstOrDefault();
            ViewBag.ToplamStok = iphoneStok.ToplamStok;
            ViewBag.KalanStok = iphoneStok.KalanStok;

            //ürünler tablosundaki ürün çeşit sayısı ve silinmemiş ürün sayısı
            ViewBag.urunSayisi=db.tblUrunler.Count();
            ViewBag.durumuTrueOlanUrunler = db.tblUrunler.Count(x => x.urunDurumu == true);
            
            //toplam müşteri sayısı ve müşterilerin toplam bakiyesi
            ViewBag.musteriSayisi = db.tblMusteri.Count(x => x.musteriDurum == true);
            ViewBag.toplamBakiye = db.tblMusteri.Where(x => x.musteriDurum == true).Sum(x => x.musteriBakiye);

            //admin tablosundaki admin sayısı
            ViewBag.AdminSayisi = db.tblAdminGiris.Count();

            //toplam satış sayısı
            ViewBag.satisSayisi = db.tblSatis.Count();

            //toplam personel sayısı
            ViewBag.personelSayisi = db.tblPersonel.Count();
            //toplam kategori sayisi
            ViewBag.kategoriSayisi = db.tblKategori.Count();
   

            return View();
        }
    }
}