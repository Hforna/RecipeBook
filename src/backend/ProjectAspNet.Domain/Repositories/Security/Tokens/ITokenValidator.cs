using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Security.Tokens
{
    public interface ITokenValidator
    {
        public Guid Validate(string token);
    }
}
