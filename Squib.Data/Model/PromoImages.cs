using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Model
{
    public class PromoImages
    {
        public string Filename { get; set; }
        public string Path { get; set; }
        public int Sort { get; set; }
        public bool IsMain { get; set; }
    }
}
