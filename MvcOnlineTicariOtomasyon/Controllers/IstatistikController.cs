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
        public ActionResult Index()
        {
            // Toplam Cari Sayısı
            var deger1 = db.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            // Toplam Ürün Sayısı
            var deger2 = db.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            // Toplam Personel Sayısı
            var deger3 = db.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            // Toplam Kategori Sayısı
            var deger4 = db.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;

            // Toplam Stok Sayısı
            var deger5 = db.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = deger5;

            // Toplam Marka Sayısı
            var deger6 = db.Uruns.Select(x => x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;

            // Kritik Seviyede Bulunan Ürün Sayısı
            var deger7 = db.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;

            // Max Fiyatlı Ürün
            var deger8 = db.Uruns.Where(x => x.SatisFiyat == db.Uruns.Max(y => y.SatisFiyat)).Select(z => z.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;

            // Min Fiyatlı Ürün
            var deger9 = db.Uruns.Where(x => x.SatisFiyat == db.Uruns.Min(y => y.SatisFiyat)).Select(z => z.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;

            // Max Marka Sayısı
            var deger12 = db.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            // Buzdolabı Sayısı
            var deger10 = db.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;

            // Laptop Sayısı
            var deger11 = db.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;

            // En Çok Satan
            var deger13 = db.Uruns.Where(x => x.UrunID == db.SatisHarekets.GroupBy(y => y.UrunID).OrderByDescending(z => z.Count()).Select(t => t.Key).FirstOrDefault()).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;

            // Kasadaki Tutar
            var deger14 = db.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;

            // Bugünkü Satışlar
            DateTime bugun = DateTime.Today;
            var deger15 = db.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
            ViewBag.d15 = deger15;

            // Bugünkü Kasa
            var deger16 = db.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar).ToString();
            ViewBag.d16 = deger16;

            return View();
        }
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