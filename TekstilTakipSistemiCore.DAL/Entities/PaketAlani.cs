using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Entities
{
    public class PaketAlani : BaseEntity
    {
        public int YikanmaSayisi { get; set; }
        public string OdaNumarasi { get; set; }
        public string PaketNo { get; set; }
        public int MefrusatId { get; set; }
        public int KimliklendirmeId { get; set; }
        public int KullaniciId { get; set; }
    }
}
