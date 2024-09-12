using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Users
{
    public interface IUserIdentifierExists
    {
        public Task<bool> UserIdentifierExists(Guid uid);
    }
}
