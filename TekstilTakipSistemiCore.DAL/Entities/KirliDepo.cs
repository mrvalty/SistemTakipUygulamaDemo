using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Enums;

namespace TekstilTakipSistemiCore.DAL.Entities
{
    public class KirliDepo : BaseEntity
    {
        public int KirlenmeSayisi { get; set; }
        public EHasarDurumu HasarDurumu { get; set; }
        public ETeslimDurumu TeslimDurumu { get; set; }
        public bool CamasirKirliMi { get; set; }
        public DateTime TartimTarihi { get; set; }
        public long YipranmaAgirligi { get; set; }
        public bool KullanimdaMi { get; set; }
        public bool HurdaMi { get; set; }

        public int KimliklendirmeId { get; set; }
        public int KullaniciId { get; set; }


    }
}
