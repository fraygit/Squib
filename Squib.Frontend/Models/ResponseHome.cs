using Squib.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squib.Frontend.Models
{
    public class ResponseHome
    {
        public ResPromoList Featured { get; set; }
        public List<ResPromoList> Promos { get; set; }
    }
}