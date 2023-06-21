using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Enums;

namespace TekstilTakipSistemiCore.UI.Models
{
    public class TemizlikBilgileriListViewModel
    {
        public string EtiketNo { get; set; }
        public string MefrusatTur { get; set; }
        public EYikamaDurumu YıkamaDurumu { get; set; }
        public ETeslimDurumu TeslimDurumu { get; set; }
        public DateTime TartimTarihi { get; set; }
        public bool AmbalajlandiMi { get; set; }
        public bool BohcalandiMi { get; set; }
    }
}
