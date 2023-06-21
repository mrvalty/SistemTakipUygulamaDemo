using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Models;

namespace TekstilTakipSistemiCore.DAL.Response
{
    public class PaketDetayResponse: BaseResponse
    {
        public List<PaketAlaniSecimListViewModel> paketlist { get; set; }

        public string kullaniciAd { get; set; }
        public string kullaniciSoyad { get; set; }
    }
}
