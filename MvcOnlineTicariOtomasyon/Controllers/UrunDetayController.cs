using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunDetayController : Controller
    {
        Context db = new Context();
        public ActionResult Index(int id)
        {
            var degerler = db.Uruns.Where(x => x.UrunID == id).ToList();
            return View(degerler);
        }
    }
}