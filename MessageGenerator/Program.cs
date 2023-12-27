using MessageGenerator;
using MessageGenerator.Model;
using MessageGenerator.ResourceAccess.ServiceBus;
using RandomPersonLib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = Host.CreateDefaultBuilder(args);

    builder.ConfigureAppConfiguration(cfg =>
    {
        cfg.AddJsonFile("appsettings.json");
    });

    builder.ConfigureServices((hostContext,services) =>
    {
        services.AddSingleton<DeposantGenerator>();
        services.AddSingleton<MessageGeneratorService>();
        services.AddSingleton<IRandomPerson, RandomPerson>();
        services.AddSingleton<ServiceBus>();
        services.AddSingleton<MessageSender>();

        services.Configure<MessageBusSettings>(hostContext.Configuration.GetSection("MessageBusSettings"));
    });
    
 IHost host = builder.Build();

//host.Run();
var service = host.Services.GetRequiredService<MessageGenerator.MessageGeneratorService>();
await service.GenerateAndPublish();
