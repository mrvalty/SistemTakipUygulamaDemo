using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Enums;

namespace TekstilTakipSistemiCore.UI.Models
{
    public class KirlenmeBilgileriListViewModel
    {
        public string EtiketNo { get; set; }
        public string MefrusatTur { get; set; }
        public EHasarDurumu HasarDurumu { get; set; }
        public ETeslimDurumu TeslimDurumu { get; set; }
        public DateTime TartimTarihi { get; set; }
        public bool KullanimdaMi { get; set; }
        public bool HurdaMi { get; set; }
        public string Olusturan { get; set; }
        
    }
}
