using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class MesajController : Controller
    {
        Context db = new Context();
        public ActionResult GelenKutusu()
        {
            var admin = "Admin";
            var mesajlar = db.Mesajlars.Where(x => x.Alici == admin).OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult GidenKutusu()
        {
            var admin = "Admin";
            var mesajlar = db.Mesajlars.Where(x => x.Gonderici == admin).OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult DuyuruKutusu()
        {
            var admin = "Admin";
            var mesajlar = db.Mesajlars.Where(x => x.Alici == "Duyuru").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult TanitimKutusu()
        {
            var admin = "Admin";
            var mesajlar = db.Mesajlars.Where(x => x.Alici == "Tanıtım").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult SosyalKutusu()
        {
            var admin = "Admin";
            var mesajlar = db.Mesajlars.Where(x => x.Alici == admin && x.Gonderici != "Admin").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {

            var admin = "Admin";
            var mesaj = db.Mesajlars.Find(id);
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View(mesaj);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var admin = "Admin";
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            m.Gonderici = "Admin";
            m.Tarih = DateTime.Parse(DateTime.Now.ToString());
            if (db.Carilers.Any(x => x.CariMail == m.Alici) == true)
            {
                db.Mesajlars.Add(m);
                db.SaveChanges();
                return RedirectToAction("GidenKutusu");
            }
            else
            {
                return RedirectToAction("YeniMesaj");
            }
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            var admin = "Admin";
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(Mesajlar m)
        {
            m.Gonderici = "Admin";
            m.Alici = "Duyuru";
            m.Tarih = DateTime.Parse(DateTime.Now.ToString());
            db.Mesajlars.Add(m);
            db.SaveChanges();
            return RedirectToAction("GidenKutusu");
        }
        [HttpGet]
        public ActionResult YeniTanitim()
        {
            var admin = "Admin";
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == admin).ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == admin).ToString();
            ViewBag.DuyuruSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == admin && x.Gonderici != "Admin").ToString();
            return View();
        }
        [HttpPost]
        public ActionResult YeniTanitim(Mesajlar m)
        {
            m.Alici = "Tanıtım";
            m.Gonderici = "Admin";
            m.Tarih = DateTime.Parse(DateTime.Now.ToString());
            db.Mesajlars.Add(m);
            db.SaveChanges();
            return RedirectToAction("GidenKutusu");
        }
    }
}