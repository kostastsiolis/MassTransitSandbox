using MassTransit;
using MTS.Contracts;

namespace MTS.Consumer.Consumers
{
    public class MessageSentConsumerDefinition : ConsumerDefinition<MessageSentConsumer>
    {
        public MessageSentConsumerDefinition()
        {
            // override the default endpoint name
            EndpointName = Constants.ENDPOINT_NAME_SEND;

            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 1000;

        }
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MessageSentConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            endpointConfigurator.UseRateLimit(1000, TimeSpan.FromSeconds(1));// 1000 requests per user per minute
        }
    }
}