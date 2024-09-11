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
    public class GenerateToken : ITokenGenerator
    {
        private uint _minutesExpirate;
        private string? _signKey;

        public string Generate(Guid uid)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Sid, uid.ToString())};

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_minutesExpirate),
                SigningCredentials = new SigningCredentials(SecurityKeyConverter(_signKey!), SecurityAlgorithms.EcdsaSha256Signature)
            };

            var tokenManipulation = new JwtSecurityTokenHandler();

            var createToken = tokenManipulation.CreateToken(descriptor);

            return tokenManipulation.WriteToken(createToken);
        }
        public SecurityKey SecurityKeyConverter(string signKey)
        {
            var toBytes = Encoding.UTF8.GetBytes(signKey);

            return new SymmetricSecurityKey(toBytes);
        }
    }
}
