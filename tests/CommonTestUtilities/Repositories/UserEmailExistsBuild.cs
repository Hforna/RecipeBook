using Moq;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class UserEmailExistsBuild
    {
        private readonly Mock<IUserEmailExists> _repository;

        public UserEmailExistsBuild() => _repository = new Mock<IUserEmailExists>();

        public void EmailExists(string email)
        {
            _repository.Setup(rep => rep.EmailExists(email)).ReturnsAsync(true);
        }

        public IUserEmailExists Build() => _repository.Object;
    }
}
