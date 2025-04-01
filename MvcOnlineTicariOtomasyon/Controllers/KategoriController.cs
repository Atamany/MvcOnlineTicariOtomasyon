using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        Context db = new Context();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.Kategoris.ToList().ToPagedList(sayfa,10);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            db.Kategoris.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.Kategoris.Find(id);
            db.Kategoris.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }
        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktgr = db.Kategoris.Find(k.KategoriID);
            ktgr.KategoriAd = k.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriListesi()
        {
            var degerler = db.Kategoris.ToList();
            return View(degerler);
        }
    }
}