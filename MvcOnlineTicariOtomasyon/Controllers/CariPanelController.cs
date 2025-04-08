using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context db = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = db.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = db.SatisHarekets.Where(x => x.CariID == id).ToList();
            return View(degerler);
        }
        public ActionResult GelenKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == mail || x.Alici == "Tanıtım").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult GidenKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult OnemliKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == mail && x.Gonderici == "Admin").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult TanitimKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == "Tanıtım").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult SosyalKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == mail && x.Gonderici != "Admin").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {

            var mail = (string)Session["CariMail"];
            var mesaj = db.Mesajlars.Find(id);
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            if (mesaj.Alici == mail || mesaj.Alici == "Tanıtım" || mesaj.Gonderici == mail)
            {
                return View(mesaj);
            }
            else
            {
                return RedirectToAction("GelenKutusu");
            }
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici == "Admin").ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var Gonderen = (string)Session["CariMail"];
            m.Gonderici = Gonderen;
            m.Tarih = DateTime.Parse(DateTime.Now.ToString());
            if (db.Carilers.Any(x => x.CariMail == m.Alici) == true || m.Alici == "Admin")
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
    }
}