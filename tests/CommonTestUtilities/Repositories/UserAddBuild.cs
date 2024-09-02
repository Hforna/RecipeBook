using Moq;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class UserAddBuild
    {
        public static IUserAdd Build()
        {
            var mock = new Mock<IUserAdd>();
            return mock.Object;
        }
    }
}
