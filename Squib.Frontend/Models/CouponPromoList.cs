using Squib.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Squib.Frontend.Models
{
    public class CouponPromoList
    {
        public Promo Promo { get; set; }
        public Coupon Coupon { get; set; }
    }
}