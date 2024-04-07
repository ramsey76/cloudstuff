using Function2;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddTransient<Calculate>();
    });
    

var host = hostBuilder.Build();
await host.RunAsync();
