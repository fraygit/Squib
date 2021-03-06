﻿using Squib.Data.Model;
using Squib.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Interface
{
    public interface ICouponRepository : IEntityService<Coupon>
    {
        Task<List<Coupon>> GetByUser(string user);
    }
}
