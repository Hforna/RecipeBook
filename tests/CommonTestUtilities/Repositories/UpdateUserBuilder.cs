using Moq;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class UpdateUserBuilder
    {
        public static IGetUserUpdate Build()
        {
            var mock = new Mock<IGetUserUpdate>();
            return mock.Object;
        }
    }
}
