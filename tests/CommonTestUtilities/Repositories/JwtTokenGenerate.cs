using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Infrastructure.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class JwtTokenGenerate
    {
        public static ITokenGenerator Build()
        {
            return new GenerateToken(40, "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww");
        }
    }
}
