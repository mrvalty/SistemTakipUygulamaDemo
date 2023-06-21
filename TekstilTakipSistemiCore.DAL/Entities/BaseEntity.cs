using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime OlusturmaZamani { get; set; }
        public DateTime GuncellemeZamani { get; set; }
        public bool AktifMi { get; set; }
    }
}
