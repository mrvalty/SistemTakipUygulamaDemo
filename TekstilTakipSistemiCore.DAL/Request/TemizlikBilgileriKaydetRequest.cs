using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Request
{
    public class TemizlikBilgileriKaydetRequest
    {
        public string EtiketNo { get; set; }
        public string YikamaDurumu { get; set; }
        public string TeslimDurumu { get; set; }
        public DateTime TartimTarihi { get; set; }
        public bool AmbalanlandiMi { get; set; }
        public bool BohcalandiMi { get; set; }
    }
}
