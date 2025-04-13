using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class IstatistikController : Controller
    {
        Context db = new Context();
        public ActionResult KolayTablolar()
        {
            var sorgu = from x in db.Carilers
                        group x by x.CariSehir into g
                        orderby g.Count() descending
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.Take(5).ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in db.Personels
                         group x by x.Departman.DepartmanAd into g
                         orderby g.Count() descending
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.Take(5).ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu3 = db.Carilers.Take(5).ToList();
            return PartialView(sorgu3);
        }
        public PartialViewResult Partial3()
        {
            var sorgu4 = db.Uruns.Take(5).ToList();
            return PartialView(sorgu4);
        }
        public PartialViewResult Partial4()
        {
            var sorgu5 = from x in db.Uruns
                         group x by x.Marka into g
                         orderby g.Count() descending
                         select new SinifGrup3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu5.Take(5).ToList());
        }
        public PartialViewResult Partial5()
        {
            var sorgu6 = from x in db.Uruns
                         group x by x.Kategori.KategoriAd into g
                         orderby g.Count() descending
                         select new SinifGrup4
                         {
                             Kategori = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu6.Take(5).ToList());
        }
    }
}