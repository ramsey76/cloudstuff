using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLWriter.Database;

namespace SQLWriter;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureServices(
            (hostcontext, services) => {
                services.AddSingleton<SQLPublisher>();
                
                services.AddDbContext<BankContext>(options => 
                    {
                        options.UseSqlServer(@"Server=sql-cloudnative-002.database.windows.net;Database=SqlDb-cloudnative-002;User Id=mainuser;Password=Marvel0!");
                    }
                );
            }
        );

        var host = builder.Build();
        var service = host.Services.GetRequiredService<SQLPublisher>();

        service.WriteEntities();
        
    }
}
