using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Entities
{
    public class Kimliklendirme :BaseEntity
    {
        public string EtiketNo { get; set; }
        public string HasarAciklama { get; set; }

        [ForeignKey("Mefrusat")]
        public int MefrusatId { get; set; }
        public Mefrusat Mefrusat { get; set; }

    }
}
