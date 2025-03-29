using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var deger1 = db.Carilers.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = db.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = db.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = (from x in db.Carilers select x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = deger4;

            var yapilacaklar = db.Yapilacaks.ToList();
            return View(yapilacaklar);
        }
    }
}