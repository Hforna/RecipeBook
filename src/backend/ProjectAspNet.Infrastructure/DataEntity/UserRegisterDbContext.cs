using Microsoft.EntityFrameworkCore;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class UserRegisterDbContext : IUserAdd, IUserEmailExists
    {
        private ProjectAspNetDbContext _dbContext;

        public UserRegisterDbContext(ProjectAspNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserEntitie user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email.Equals(email));
        }

        public async Task<UserEntitie?> LoginByEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Active && x.Password.Equals(password) && x.Email.Equals(email));
        }
    }
}
