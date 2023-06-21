using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekstilTakipSistemiCore.DAL.Models;

namespace TekstilTakipSistemiCore.DAL.Request
{
    public class PaketOlusturRequest
    {
        public string dataid { get; set; }
        public string odano { get; set; }
        public List<PaketOlusturVeriViewModel> _paketList { get; set; }
    }
}
