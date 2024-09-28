
using Azure.Messaging.ServiceBus;
using ProjectAspNet.Application.UseCases.Repositories.User;
using ProjectAspNet.Infrastructure.ServiceBus;

namespace ProjectAspNet.BackgroundServices
{
    public class DeleteUserService : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;

        public DeleteUserService(IServiceProvider serviceProvider, DeleteUserProcessor processor)
        {
            _processor = processor.GetProcessor();
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;

            await _processor.StartProcessingAsync(stoppingToken);
        }

        public async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();

            var createProvider = _serviceProvider.CreateScope();

            var deleteUseCase = _serviceProvider.GetRequiredService<IDeleteUserUseCase>();

            var userIdentifier = Guid.Parse(message);

            await deleteUseCase.Execute(userIdentifier);
        }

        ~DeleteUserService() => Dispose();

        public override void Dispose()
        {
            base.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
