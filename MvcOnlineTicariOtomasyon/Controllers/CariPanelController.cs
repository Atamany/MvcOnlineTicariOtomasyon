using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var cari = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = db.Mesajlars.Where(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").OrderByDescending(x => x.Tarih).ToList();
            var profil = db.Carilers.Where(x => x.CariMail == mail).FirstOrDefault();
            ViewBag.SiparisSayisi = db.SatisHarekets.Count(x => x.CariID == cari).ToString();
            ViewBag.GonderilenMesaj = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.AlinanMesaj = db.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.Ad = profil.CariAd;
            ViewBag.Soyad = profil.CariSoyad;
            ViewBag.Mail = profil.CariMail;
            ViewBag.Sehir = profil.CariSehir;
            if (profil.CariResim == null)
            {
                ViewBag.ProfilFotografi = "https://static.vecteezy.com/system/resources/previews/005/005/788/non_2x/user-icon-in-trendy-flat-style-isolated-on-grey-background-user-symbol-for-your-web-site-design-logo-app-ui-illustration-eps10-free-vector.jpg";
            }
            else
            {
                ViewBag.ProfilFotografi = profil.CariResim;
            }
            return View(degerler);
        }
        [HttpPost]
        public ActionResult ProfilGuncelle(Cariler c, string password1, string password2)
        {
            var mail = (string)Session["CariMail"];
            var cariId = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var cari = db.Carilers.Find(cariId);
            if (password1 == password2)
            {
                cari.CariAd = c.CariAd;
                cari.CariSoyad = c.CariSoyad;
                cari.CariSehir = c.CariSehir;
                cari.CariMail = c.CariMail;
                cari.CariResim = c.CariResim;
                cari.CariSifre = password1;
                db.SaveChanges();
                return RedirectToAction("Logout", "Login");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = db.SatisHarekets.Where(x => x.CariID == id).ToList();
            return View(degerler);
        }
        public ActionResult KargoTakip()
        {
            var mail = (string)Session["CariMail"];
            var cari = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = db.KargoDetays.Where(x => x.CariID == cari).OrderByDescending(x => x.Tarih).ToList();
            return View(degerler);
        }
        public ActionResult KargoDetay(string id)
        {
            var mail = (string)Session["CariMail"];
            var cari = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = db.KargoTakips.Where(x => x.TakipKodu == id).OrderByDescending(x => x.TarihZaman).ToList();
            var kargo = db.KargoDetays.Where(x => x.TakipKodu == id).FirstOrDefault();
            if (kargo.CariID == cari)
            {
                return View(degerler);
            }
            else
            {
                return RedirectToAction("KargoTakip");
            }
        }
        public ActionResult GelenKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult GidenKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult OnemliKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult TanitimKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == "Tanıtım").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult SosyalKutusu()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = db.Mesajlars.Where(x => x.Alici == mail && x.Gonderici != "Admin").OrderByDescending(x => x.Tarih).ToList();
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {

            var mail = (string)Session["CariMail"];
            var mesaj = db.Mesajlars.Find(id);
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
            ViewBag.TanitimSayisi = db.Mesajlars.Count(x => x.Alici == "Tanıtım").ToString();
            ViewBag.SosyalSayisi = db.Mesajlars.Count(x => x.Alici == mail && x.Gonderici != "Admin").ToString();
            if (mesaj.Alici == mail || mesaj.Alici == "Tanıtım" || mesaj.Alici == "Duyuru" || mesaj.Gonderici == mail)
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
            ViewBag.GelenSayisi = db.Mesajlars.Count(x => x.Alici == mail || x.Alici == "Tanıtım" || x.Alici == "Duyuru").ToString();
            ViewBag.GidenSayisi = db.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.OnemliSayisi = db.Mesajlars.Count(x => x.Alici == "Duyuru" || (x.Alici == mail && x.Gonderici == "Admin")).ToString();
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
        public PartialViewResult Ayarlar()
        {
            var mail = (string)Session["CariMail"];
            var cari = db.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariID).FirstOrDefault();
            var degerler = db.Carilers.Find(cari);
            return PartialView("Ayarlar", degerler);
        }
        public PartialViewResult Duyurular()
        {
            var duyurular = db.Mesajlars.Where(x => x.Alici == "Duyuru").OrderByDescending(x => x.Tarih).ToList();
            return PartialView("Duyurular", duyurular);
        }
    }
}