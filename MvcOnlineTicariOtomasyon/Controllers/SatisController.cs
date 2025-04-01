using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var degerler = db.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in db.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Marka + " " + x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in db.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in db.Personels.Where(x => x.DepartmanID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SatisHarekets.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in db.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Marka + " " + x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in db.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in db.Personels.Where(x => x.DepartmanID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            var satis = db.SatisHarekets.Find(id);
            return View("SatisGetir", satis);
        }
        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var satis = db.SatisHarekets.Find(s.SatisID);
            satis.Adet = s.Adet;
            satis.Fiyat = s.Fiyat;
            satis.Tarih = s.Tarih;
            satis.ToplamTutar = s.ToplamTutar;
            satis.UrunID = s.UrunID;
            satis.CariID = s.CariID;
            satis.PersonelID = s.PersonelID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = db.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }
    }
}