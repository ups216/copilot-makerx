using GameManager;
using GameManager.Consumers;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<MatchWaitingForGameConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureServiceEndpoints(context);
            });
        });
    })
    .Build();

host.Run();
