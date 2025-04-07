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
            var mesajlar = db.Mesajlars.ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var Gonderen = (string)Session["CariMail"];
            m.Gonderici = Gonderen;
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.Mesajlars.Add(m);
            db.SaveChanges();
            return RedirectToAction("GelenKutusu");
        }
    }
}