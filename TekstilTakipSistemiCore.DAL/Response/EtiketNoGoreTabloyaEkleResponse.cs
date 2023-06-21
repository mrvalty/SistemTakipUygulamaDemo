using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Models;

namespace TekstilTakipSistemiCore.DAL.Response
{
    public class EtiketNoGoreTabloyaEkleResponse
    {
        public string zbodyYazisi { get; set; }
        public int zSonuc { get; set; }
        public string zAciklama { get; set; }

        public List<PaketAlaniSecimListViewModel> zDizi { get; set; }

    }
}
