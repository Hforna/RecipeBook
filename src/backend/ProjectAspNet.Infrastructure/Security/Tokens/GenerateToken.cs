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
    public class GenerateToken : JwtSecurityKeyConverter, ITokenGenerator
    {
        private uint _expirateMinutes;
        private string _signKey = string.Empty;

        public GenerateToken(uint expirateMinutes, string signKey)
        {
            _expirateMinutes = expirateMinutes;
            _signKey = signKey;
        }

        public string Generate(Guid uid)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Sid, uid.ToString()) };
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirateMinutes),
                SigningCredentials = new SigningCredentials(getAsSecurityKey(_signKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var createToken = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(createToken);
        }
    }
}
