using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectAspNetDbContext _dbContext;

        public UnitOfWork(ProjectAspNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
