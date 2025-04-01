using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context db = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in db.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from x in db.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.KategoriAd,
                                                 Value = x.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            p.Durum = true;
            db.Uruns.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urun = db.Uruns.Find(id);
            urun.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> degerler = (from x in db.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.KategoriAd,
                                                 Value = x.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            var urunDeger = db.Uruns.Find(id);
            return View("UrunGetir", urunDeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urn = db.Uruns.Find(p.UrunID);
            urn.UrunAd = p.UrunAd;
            urn.Marka = p.Marka;
            urn.AlisFiyat = p.AlisFiyat;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Stok = p.Stok;
            urn.KategoriID = p.KategoriID;
            urn.UrunGorsel = p.UrunGorsel;
            urn.UrunBilgi = p.UrunBilgi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = db.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            var deger1 = db.Uruns.Find(id);
            List<SelectListItem> deger3 = (from x in db.Personels.Where(x => x.DepartmanID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1.UrunID;
            ViewBag.dgr2 = deger1.SatisFiyat;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.ToplamTutar = Convert.ToDecimal(p.Adet) * Convert.ToDecimal(p.Fiyat);
            db.SatisHarekets.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Satis");
        }
    }
}