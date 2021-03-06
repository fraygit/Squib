﻿using Squib.Data.Model;
using Squib.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Interface
{
    public interface IPaymentTransactionRepository : IEntityService<PaymentTransaction>
    {
    }
}
