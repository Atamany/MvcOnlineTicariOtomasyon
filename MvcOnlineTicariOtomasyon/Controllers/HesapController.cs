using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class HesapController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var admin = (string)Session["KullaniciAd"];
            var adminler = db.Admins.Where(x => x.KullaniciAd == admin.ToString()).Select(y => y.AdminID).FirstOrDefault();
            var degerler = db.Mesajlars.Where(x => x.Alici == "Admin").OrderByDescending(x => x.Tarih).ToList();
            var profil = db.Admins.Where(x => x.KullaniciAd == admin).FirstOrDefault();
            ViewBag.GonderilenMesaj = db.Mesajlars.Count(x => x.Gonderici == "Admin").ToString();
            ViewBag.AlinanMesaj = db.Mesajlars.Count(x => x.Alici == "Admin").ToString();
            ViewBag.AdminID = adminler;
            ViewBag.KullaniciAdi = admin;
            if (profil.AdminResim == null)
            {
                ViewBag.ProfilFotografi = "https://static.vecteezy.com/system/resources/previews/005/005/788/non_2x/user-icon-in-trendy-flat-style-isolated-on-grey-background-user-symbol-for-your-web-site-design-logo-app-ui-illustration-eps10-free-vector.jpg";
            }
            else
            {
                ViewBag.ProfilFotografi = profil.AdminResim;
            }
            return View(degerler);
        }
        [HttpPost]
        public ActionResult ProfilGuncelle(Admin a, string password1, string password2)
        {
            var adminAd = (string)Session["KullaniciAd"];
            var adminId = db.Admins.Where(x => x.KullaniciAd == adminAd.ToString()).Select(y => y.AdminID).FirstOrDefault();
            var admin = db.Admins.Find(adminId);
            if (password1 == password2)
            {
                admin.KullaniciAd = a.KullaniciAd;
                admin.Sifre = password1;
                admin.AdminResim = a.AdminResim;
                db.SaveChanges();
                return RedirectToAction("Logout", "Login");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public PartialViewResult Ayarlar()
        {
            var adminAd = (string)Session["KullaniciAd"];
            var adminId = db.Admins.Where(x => x.KullaniciAd == adminAd.ToString()).Select(y => y.AdminID).FirstOrDefault();
            var degerler = db.Admins.Find(adminId);
            return PartialView("Ayarlar", degerler);
        }
        public PartialViewResult Duyurular()
        {
            var duyurular = db.Mesajlars.Where(x => x.Alici == "Duyuru").OrderByDescending(x => x.Tarih).ToList();
            return PartialView("Duyurular", duyurular);
        }
    }
}