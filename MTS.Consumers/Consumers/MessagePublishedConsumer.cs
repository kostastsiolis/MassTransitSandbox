using MassTransit;
using MTS.Contracts.Interfaces;

namespace MTS.Consumer.Consumers
{
    public class MessagePublishedConsumer : IConsumer<IMessagePublished>
    {
        readonly ILogger<MessagePublishedConsumer> _logger;

        public MessagePublishedConsumer(ILogger<MessagePublishedConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<IMessagePublished> context)
        {
            _logger.LogInformation("Received Published Text: {Text}", context.Message.Value);

            return Task.CompletedTask;
        }
    }
}