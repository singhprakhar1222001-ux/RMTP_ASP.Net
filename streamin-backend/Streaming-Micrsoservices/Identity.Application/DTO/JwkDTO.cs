using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.DTO
{
    public class JwkDTO
    {
        public string Kty { get; set; }
        public string Use { get; set; }
        public string N {  get; set; }

        public string Kid { get; set; }

        public string E {  get; set; }
    }
}
