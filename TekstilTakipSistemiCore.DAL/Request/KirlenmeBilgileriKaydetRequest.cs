using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Request
{
    public class KirlenmeBilgileriKaydetRequest
    {
        public string EtiketNo { get; set; }
        public string HasarDurumu { get; set; }
        public string TeslimDurumu { get; set; }
        public DateTime TartimTarihi { get; set; }
        public bool KullanimdaMi { get; set; }
        public bool HurdaMi { get; set; }
        public string Olusturan { get; set; }
    }
}
