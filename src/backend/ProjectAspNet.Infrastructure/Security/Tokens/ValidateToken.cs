using Microsoft.IdentityModel.Tokens;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Security.Tokens
{
    public class ValidateToken : JwtSecurityKeyConverter, ITokenValidator
    {
        private readonly string _signKey;

        public ValidateToken(string signKey) => _signKey = signKey;

        public Guid Validate(string token)
        {
            var descriptor = new TokenValidationParameters
            {
                ClockSkew = new TimeSpan(0),
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = getAsSecurityKey(_signKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var createToken = tokenHandler.ValidateToken(token, descriptor, out _);

            var tokenValue = createToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            return Guid.Parse(tokenValue);
        }
    }
}
