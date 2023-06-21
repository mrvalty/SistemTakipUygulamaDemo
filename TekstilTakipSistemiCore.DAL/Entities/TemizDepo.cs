using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Enums;

namespace TekstilTakipSistemiCore.DAL.Entities
{
    public class TemizDepo : BaseEntity
    {
        public int YikanmaSayisi { get; set; }
        public EYikamaDurumu YikamaDurumu { get; set; }
        public ETeslimDurumu TeslimDurumu { get; set; }
        public bool CamasirYikandiMi { get; set; }
        public DateTime TartimTarihi { get; set; }
        public long YipranmaAgirligi { get; set; }
        public bool AmbalajlandiMi { get; set; }
        public bool BohcalandiMi { get; set; }

        public int KimliklendirmeId { get; set; }
        public int KullaniciId { get; set; }

    }
}
