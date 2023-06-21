using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Models
{
    public class PaketAlaniSecimListViewModel
    {
        public int rownum { get; set; }
        public string EtiketNo { get; set; }
        public string MefrusatTuru { get; set; }
        public string PaketNo { get; set; }
        public string OdaNo { get; set; }
        public string Olusturan { get; set; }
        public DateTime OlusturmaZamani { get; set; }
    }
}
