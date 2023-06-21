using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.UI.Models
{
    public class PaketAlaniListViewModel
    {
        public string EtiketNo { get; set; }
        public string PaketNo { get; set; }
        public string OdaNumarasi { get; set; }
        public string MefrusatTuru { get; set; }
        public int YikanmaSayisi { get; set; }
        public DateTime OlusturmaZamani { get; set; }
       
    }
}
