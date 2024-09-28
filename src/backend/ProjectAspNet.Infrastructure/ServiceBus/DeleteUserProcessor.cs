using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.ServiceBus
{
    public class DeleteUserProcessor
    {
        private readonly ServiceBusProcessor _processor;

        public DeleteUserProcessor(ServiceBusProcessor processor) => _processor = processor;

        public ServiceBusProcessor GetProcessor() => _processor;
    }
}
