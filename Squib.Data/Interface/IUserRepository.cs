﻿using Squib.Data.Model;
using Squib.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Interface
{
    public interface IUserRepository : IEntityService<User>
    {
        Task<User> GetUser(string username);
    }
}
