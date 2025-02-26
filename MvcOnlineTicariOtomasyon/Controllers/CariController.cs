using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var degerler = db.Carilers.ToList();
            return View(degerler);
        }
    }
}