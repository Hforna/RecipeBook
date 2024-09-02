using Moq;
using ProjectAspNet.Domain.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class ProductAddBuild
    {
        public static IProductAdd Build()
        {
            var mock = new Mock<IProductAdd>();
            return mock.Object;
        }
    }
}
