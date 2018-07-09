using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squib.Backoffice.Models
{
    public class CreatePromoViewModel
    {
        public List<Organisation> Organisations { get; set; }
        public Promo Promo { get; set; }
        public bool IsAdministrator { get; set; }
    }
}