using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squib.Backoffice.Models
{
    public class ResponseGetQRCode
    {
        public Coupon Coupon { get; set; }
        public User User { get; set; }
        public Promo Promo { get; set; }
    }
}