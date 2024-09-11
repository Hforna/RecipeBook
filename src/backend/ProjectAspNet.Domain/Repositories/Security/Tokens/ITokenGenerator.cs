using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Security.Tokens
{
    public interface ITokenGenerator
    {
        public string Generate(Guid uid);
    }
}
