using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.DataEntity
{
    public class ProductRegisterDbContext : IProductAdd
    {
        private readonly ProjectAspNetDbContext _dbContext;

        public ProductRegisterDbContext(ProjectAspNetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ProductEntitie product)
        {
            await _dbContext.AddAsync(product);
        }
    }
}
