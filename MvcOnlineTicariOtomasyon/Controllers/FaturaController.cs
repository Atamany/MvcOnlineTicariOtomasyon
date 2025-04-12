using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
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
            List<SelectListItem> deger3 = (from x in db.Personels.Where(x => x.DepartmanID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
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
            ftr.PersonelID = fatura.PersonelID;
            ftr.CariID = fatura.CariID;
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
            var fatura = db.Faturas.FirstOrDefault(f => f.FaturaID == faturaKalem.FaturaID);
            if (fatura != null)
            {
                fatura.Toplam += faturaKalem.Tutar;
            }
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