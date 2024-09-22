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
        private readonly uint _minutesExpirate;
        private readonly string _signKey;

        public GenerateToken(uint minutesExpirate, string signKey)
        {
            _minutesExpirate = minutesExpirate;
            _signKey = signKey;
        }
        public string Generate(Guid uid)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Sid, uid.ToString())};

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_minutesExpirate),
                SigningCredentials = new SigningCredentials(getAsSecurityKey(_signKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenManipulation = new JwtSecurityTokenHandler();

            var createToken = tokenManipulation.CreateToken(descriptor);

            return tokenManipulation.WriteToken(createToken);
        }
    }
}
