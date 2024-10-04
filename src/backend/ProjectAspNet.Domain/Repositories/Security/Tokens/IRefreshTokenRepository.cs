using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Security.Tokens
{
    public interface IRefreshTokenRepository
    {
        public Task AddRefreshToken(RefreshTokenEntitie token);
        public Task SaveRefreshToken(RefreshTokenEntitie token, UserEntitie user);
        public Task<bool> TokenExists(UserEntitie user);
        public Task<RefreshTokenEntitie?> Get(string token);
    }
}
