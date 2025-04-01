using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var degerler = db.KargoDetays.ToList();
            return View(degerler);
        }
        public ActionResult KargoListesi()
        {
            var degerler = db.KargoDetays.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int k1, k2, k3;
            k1=rnd.Next(0, karakterler.Length);
            k2=rnd.Next(0, karakterler.Length);
            k3=rnd.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2.ToString() + karakterler[k2] + s3.ToString() + karakterler[k3];
            List<SelectListItem> deger3 = (from x in db.Personels.Where(x => x.DepartmanID == 1).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            ViewBag.TakipKod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay k)
        {
            db.KargoDetays.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoDetay(string id)
        {
            var degerler = db.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            ViewBag.TakipKod = id;
            return View(degerler);
        }
    }
}