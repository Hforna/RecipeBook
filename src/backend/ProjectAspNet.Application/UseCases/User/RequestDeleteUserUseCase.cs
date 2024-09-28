using ProjectAspNet.Application.UseCases.Repositories.User;
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

        public RequestDeleteUserUseCase(IDeleteUserSender senderRequest, ILoggedUser loggedUser)
        {
            _senderRequest = senderRequest;
            _loggedUser = loggedUser;
        }

        public async Task Execute()
        {
            var user = await _loggedUser.getUser();

            await _senderRequest.SendMessage(user);   
        }
    }
}
