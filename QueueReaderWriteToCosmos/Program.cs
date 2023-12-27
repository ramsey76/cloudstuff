using QueueReaderWriteToCosmos;
using QueueReaderWriteToCosmos.Access.CosmoDB;
using QueueReaderWriteToCosmos.Config;

var builder = Host.CreateDefaultBuilder(args);
    
builder
    .ConfigureAppConfiguration(cfg =>
    {
        cfg.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((hostContext,services) =>
    {
        services.AddHostedService<Worker>();
        services.Configure<MessageBusSettings>(hostContext.Configuration.GetSection("MessageBusSettings"));
        services.Configure<CosmosDBSettings>(hostContext.Configuration.GetSection("CosmosDBSettings"));
        services.AddSingleton<CosmosWriter>();
    });

IHost host = builder.Build();
host.Run();
