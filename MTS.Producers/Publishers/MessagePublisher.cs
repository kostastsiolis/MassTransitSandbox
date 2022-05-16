using MassTransit;
using MTS.Contracts.Interfaces;

namespace MTS.Publisher.Publishers
{
    public class MessagePublisher : BackgroundService
    {
        readonly IBus _bus;
        readonly ILogger<MessagePublisher> _logger;

        public MessagePublisher(IBus bus, ILogger<MessagePublisher> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string val = $"The time is {DateTimeOffset.Now}";
                await _bus.Publish<IMessagePublished>(new { Value = val });

                _logger.LogInformation("Published Text: {Text}", val);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}