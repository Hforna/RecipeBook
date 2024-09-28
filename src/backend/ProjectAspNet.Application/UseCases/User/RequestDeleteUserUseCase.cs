using ProjectAspNet.Application.UseCases.Repositories.User;
using ProjectAspNet.Domain.Repositories;
using ProjectAspNet.Domain.Repositories.ServiceBus;
using ProjectAspNet.Domain.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Application.UseCases.User
{
    public class RequestDeleteUserUseCase : IRequesteDeleteUserUseCase
    {
        private readonly IDeleteUserSender _senderRequest;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserUpdate _updateUser;

        public RequestDeleteUserUseCase(IDeleteUserSender senderRequest, ILoggedUser loggedUser, IUnitOfWork unitOfWork, IGetUserUpdate userUpdate)
        {
            _senderRequest = senderRequest;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _updateUser = userUpdate;
        }

        public async Task Execute()
        {
            var user = await _loggedUser.getUser();

            user.Active = false;

            _updateUser.Update(user);

            await _unitOfWork.Commit();

            await _senderRequest.SendMessage(user);   
        }
    }
}
