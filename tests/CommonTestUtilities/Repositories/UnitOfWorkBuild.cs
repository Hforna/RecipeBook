using Moq;
using ProjectAspNet.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public static class UnitOfWorkBuild
    {
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();
            return mock.Object;
        }
    }
}
