using Azure.Messaging.ServiceBus;
using ProjectAspNet.Domain.Entities;
using ProjectAspNet.Domain.Repositories.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.ServiceBus
{
    public class DeleteUserSender : IDeleteUserSender
    {
        private readonly ServiceBusSender _sender;

        public DeleteUserSender(ServiceBusSender sender) => _sender = sender;

        public async Task SendMessage(UserEntitie user)
        {
            await _sender.SendMessageAsync(new ServiceBusMessage(user.UserIdentifier.ToString()));
        }
    }
}
