using Squib.Data.Interface;
using Squib.Data.Model;
using Squib.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squib.Data.Repository
{
    public class UserRepository : EntityService<User>, IUserRepository
    {
    }
}
