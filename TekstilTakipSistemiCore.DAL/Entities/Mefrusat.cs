using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Entities
{
    public class Mefrusat : BaseEntity
    {
        public string MefrusatTuru { get; set; }
        public string MefrusatKodu { get; set; }

        public List<Kimliklendirme> Kimliklendirmeler { get; set; }
    }
}
