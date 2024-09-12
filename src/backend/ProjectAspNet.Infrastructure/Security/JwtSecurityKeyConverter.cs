using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Security
{
    public abstract class JwtSecurityKeyConverter
    {
        public SecurityKey getAsSecurityKey(string key)
        {
            var getBytes = Encoding.UTF8.GetBytes(key);

            return new SymmetricSecurityKey(getBytes);
        }
    }
}
