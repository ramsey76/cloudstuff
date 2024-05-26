using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Target.Database;
using Target.Business;
using System;

        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureServices(
            (hostcontext, services) => {
                
                services.AddDbContext<DatabaseContext>(options => 
                    {
                        options.UseSqlServer(@"Server=tcp:sql-cloudnative-002.database.windows.net,1433;Initial Catalog=SqlDb-cloudnative-003;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication='Active Directory Default';");
                    }
                );
                services.AddTransient<Accounts>();
            }
        );
        
        var host = builder.Build();


using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext >();

    // This will apply any pending migrations.
    context.Database.Migrate();
}

using(var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var accounts = services.GetRequiredService<Accounts>();
    accounts.CountAllAccounts();
}

