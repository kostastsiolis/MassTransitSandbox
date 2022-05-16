using MassTransit;
using MTS.Contracts;
using MTS.Contracts.Interfaces;

namespace MTS.Publisher.Senders
{
    public class MessageSender : BackgroundService
    {
        readonly IBus _bus;
        readonly ILogger<MessageSender> _logger;

        public MessageSender(IBus bus, ILogger<MessageSender> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string val = $"The time is {DateTimeOffset.Now}";

                var endpoint = await _bus.GetSendEndpoint(new Uri($"exchange:{Constants.ENDPOINT_NAME_SEND}"));
                await endpoint.Send<ISendMessage>(new { Value = val });

                _logger.LogInformation("Sent Text: {Text}", val);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}