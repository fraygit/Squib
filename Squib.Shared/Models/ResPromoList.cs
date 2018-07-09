using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Shared.Models
{
    public class ResPromoList : Promo
    {
        public string OrganisationName { get; set; }
        public string StatusText { get; set; }
    }
}
