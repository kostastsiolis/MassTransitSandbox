using MassTransit;
using MTS.Contracts.Interfaces;

namespace MTS.Consumer.Consumers
{
    public class MessageSentConsumer : IConsumer<ISendMessage>
    {
        readonly ILogger<MessageSentConsumer> _logger;

        public MessageSentConsumer(ILogger<MessageSentConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<ISendMessage> context)
        {
            _logger.LogInformation("Received Sent Text: {Text}", context.Message.Value);

            return Task.CompletedTask;
        }
    }
}