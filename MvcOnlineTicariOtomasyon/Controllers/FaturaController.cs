using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var degerler = db.Faturas.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Fatura fatura)
        {
            db.Faturas.Add(fatura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = db.Faturas.Find(id);
            return View("FaturaGetir", fatura);
        }
        public ActionResult FaturaGuncelle(Fatura fatura)
        {
            var ftr = db.Faturas.Find(fatura.FaturaID);
            ftr.FaturaSeriNo = fatura.FaturaSeriNo;
            ftr.FaturaSiraNo = fatura.FaturaSiraNo;
            ftr.VergiDairesi = fatura.VergiDairesi;
            ftr.FaturaTarih = fatura.FaturaTarih;
            ftr.Saat = fatura.Saat;
            ftr.TeslimEden = fatura.TeslimEden;
            ftr.TeslimAlan = fatura.TeslimAlan;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var degerler = db.FaturaKalems.Where(x => x.FaturaID == id).ToList();
            ViewBag.id = id;
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKalem(int id)
        {
            var model = new FaturaKalem { FaturaID = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem faturaKalem)
        {
            db.FaturaKalems.Add(faturaKalem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaListesi()
        {
            var degerler = db.Faturas.ToList();
            return View(degerler);
        }
    }
}