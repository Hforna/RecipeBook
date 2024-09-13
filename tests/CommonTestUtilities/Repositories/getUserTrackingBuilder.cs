using Moq;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Repositories
{
    public class getUserTrackingBuilder
    {
        private Mock<IGetUserTracking> _repository;
        public IGetUserTracking Build(UserEntitie user)
        {
            _repository = new Mock<IGetUserTracking>();
            _repository.Setup(r => r.getUserById(user.Id)).ReturnsAsync(user);
            return _repository.Object;
        }
    }
}
