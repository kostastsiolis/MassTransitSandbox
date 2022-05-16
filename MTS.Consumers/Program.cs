using MassTransit;
using MTS.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<MessagePublishedConsumer, MessagePublishedConsumerDefinition>();
            x.AddConsumer<MessageSentConsumer, MessageSentConsumerDefinition>();

            x.SetKebabCaseEndpointNameFormatter();

            // RabbitMq
            // context is the registration context, used to configure endpoints.
            // cfg is the bus factory configurator, used to configure the bus.
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });
    })
    .Build();

await host.RunAsync();