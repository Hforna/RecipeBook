using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.Products
{
    public interface IProductAdd
    {
        public Task Add(ProductEntitie product);
    }
}
