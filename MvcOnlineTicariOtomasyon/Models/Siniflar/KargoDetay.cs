using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayID { get; set; }
        public int UrunID { get; set; }
        public virtual Urun Uruns { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string TakipKodu { get; set; }
        public int PersonelID { get; set; }
        public virtual Personel Personels { get; set; }
        public int CariID { get; set; }
        public virtual Cariler Carilers { get; set; }
        public DateTime Tarih { get; set; }
    }
}