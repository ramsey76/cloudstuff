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
                        options.UseSqlServer(@"Server=tcp:sql-cloudnative-002.database.windows.net,1433;Initial Catalog=SqlDb-cloudnative-002;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication='Active Directory Default';");
                    }
                );
            }
        );

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BankContext>();

            // This will apply any pending migrations.
            context.Database.Migrate();
        }

        var service = host.Services.GetRequiredService<SQLPublisher>();

        service.WriteEntities();
        
    }
}
