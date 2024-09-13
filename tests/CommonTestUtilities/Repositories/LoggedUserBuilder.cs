using Moq;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Users;
using ProjectAspNet.Infrastructure.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class LoggedUserBuilder
    {
        private readonly Mock<ILoggedUser> _loggedUser;

        public LoggedUserBuilder() => _loggedUser = new Mock<ILoggedUser>();
        public ILoggedUser Build(UserEntitie user)
        {
            _loggedUser.Setup(x => x.getUser()).ReturnsAsync(user);
            return _loggedUser.Object;
        }
    }
}
