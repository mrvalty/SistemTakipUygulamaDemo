using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Enums;

namespace TekstilTakipSistemiCore.DAL.Models
{
    public class TemizlikDetayRaporListViewModel
    {
        public string EtiketNo { get; set; }
        public string MefrusatTur { get; set; }
        public int MefrusatId { get; set; }
        public EYikamaDurumu YikamaDurumu { get; set; }
        public ETeslimDurumu TeslimDurumu { get; set; }
        public bool AmbalajlandiMi { get; set; }
        public bool BohcalandiMi { get; set; }

        public int YikanmaSayisi { get; set; }
        public int KirlenmeSayisi { get; set; }
        public int KimliklendirmeId { get; set; }


    }
}
