using ProjectAspNet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Domain.Repositories.ServiceBus
{
    public interface IDeleteUserSender
    {
        public Task SendMessage(UserEntitie user);
    }
}
