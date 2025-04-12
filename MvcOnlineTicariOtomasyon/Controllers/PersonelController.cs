using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var degerler = db.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> degerler = (from x in db.Departmans.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.DepartmanAd,
                                                 Value = x.DepartmanID.ToString()
                                             }).ToList();
            ViewBag.dgr1 = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if(Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "/Images/" + dosyaAdi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Images/" + dosyaAdi;
            }
            db.Personels.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var personel = db.Personels.Find(id);
            List<SelectListItem> degerler = (from x in db.Departmans.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.DepartmanAd,
                                                 Value = x.DepartmanID.ToString()
                                             }).ToList();
            ViewBag.dgr1 = degerler;
            return View("PersonelGetir", personel);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "/Images/" + dosyaAdi;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Images/" + dosyaAdi;
            }
            var prsnl = db.Personels.Find(p.PersonelID);
            prsnl.PersonelAd = p.PersonelAd;
            prsnl.PersonelSoyad = p.PersonelSoyad;
            prsnl.PersonelGorsel = p.PersonelGorsel;
            prsnl.DepartmanID = p.DepartmanID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelListe()
        {
            var sorgu = db.Personels.ToList();
            return View(sorgu);
        }
        public ActionResult PersonelListesi()
        {
            var degerler = db.Personels.ToList();
            return View(degerler);
        }
    }
}