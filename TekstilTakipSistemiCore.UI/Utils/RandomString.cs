using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TekstilTakipSistemiCore.UI.Utils
{
    class RandomString
    {
        public string RandomStringValue(int size)
        {
            Random random = new Random();
            string input = "123456789";
            var chars = Enumerable.Range(0, size)
                                   .Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }
    }
}
