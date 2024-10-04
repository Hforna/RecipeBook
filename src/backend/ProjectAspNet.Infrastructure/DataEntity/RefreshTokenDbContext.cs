using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Entities.Tokens;
using ProjectAspNet.Domain.Repositories.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class RefreshTokenDbContext : IRefreshTokenRepository
    {
        private readonly ProjectAspNetDbContext _dbContext;

        public RefreshTokenDbContext(ProjectAspNetDbContext dbContext) => _dbContext = dbContext;

        public async Task AddRefreshToken(RefreshTokenEntitie token)
        {
            await _dbContext.refreshToken.AddAsync(token);
        }

        public async Task<RefreshTokenEntitie?> Get(string token)
        {
            return await _dbContext.refreshToken.AsNoTracking().Include(d => d.User).FirstOrDefaultAsync(d => d.Value == token);
        }

        public async Task SaveRefreshToken(RefreshTokenEntitie token, UserEntitie user)
        {
            var refreshToken = _dbContext.refreshToken.Where(d => d.UserId == token.UserId);

            _dbContext.refreshToken.RemoveRange(refreshToken);

            await _dbContext.refreshToken.AddAsync(token);
        }

        public async Task<bool> TokenExists(UserEntitie user)
        {
            return await _dbContext.refreshToken.AnyAsync(d => d.UserId == user.Id);
        }
    }
}
