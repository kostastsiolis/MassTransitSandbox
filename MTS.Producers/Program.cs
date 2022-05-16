using MassTransit;
using MTS.Publisher.Publishers;
using MTS.Publisher.Senders;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
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
            });
        });

        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromSeconds(30);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

        // Hosted Services       
        services.AddHostedService<MessagePublisher>();
        services.AddHostedService<MessageSender>();
    })
    .Build();

await host.RunAsync();