using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            //var degerler = db.Uruns.Where(x=>x.UrunID==1).ToList();
            cs.Deger1=db.Uruns.Where(x => x.UrunID == 1).ToList();
            cs.Deger2 = db.Detays.Where(y => y.DetayID == 1).ToList();
            return View(cs);
        }
    }
}