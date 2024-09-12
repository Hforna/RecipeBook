using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Infrastructure.DataEntity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Security.Tokens
{
    public class LoggedUser : ILoggedUser
    {
        private readonly ITokenReceptor _tokenReceptor;
        private readonly ProjectAspNetDbContext _dbContext;

        public LoggedUser(ITokenReceptor tokenReceptor, ProjectAspNetDbContext dbContext)
        {
            _tokenReceptor = tokenReceptor;
            _dbContext = dbContext;
        }

        public async Task<UserEntitie> getUser()
        {
            var token = _tokenReceptor.Value();

            var tokenHandler = new JwtSecurityTokenHandler();
            var writeToken = tokenHandler.ReadJwtToken(token);
            var identifier = writeToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var userIdentifier = Guid.Parse(identifier);

            return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }

    }
}
