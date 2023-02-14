using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealEstateRefactored.Infrastructure;
using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Interfaces.Strategies;
using RealEstateRefactored.Services;
using RealEstateRefactored.Services.Strategies;

namespace RealEstateRefactored
{
    public class Startup {
        static void Main(string[] args)
        {
            ConfigureServices();
        }

        private static void ConfigureServices()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Infrastructure.
                    services.AddLogging(config => config.AddConsole());
                    services.AddSingleton<IDbContext, DbContext>();
                    services.AddSingleton<IDbConnection, DbConnection>();
                    services.AddSingleton<AppRunner>();

                    // Services.
                    services.AddScoped<ITableService, TableService>();

                    // Strategies.
                    services.AddScoped<ICommandContext, CommandContext>();
                    services.AddScoped<IShowTablesStrategy, ShowTablesStrategy>();
                    services.AddScoped<ICreateTableStrategy, CreateTableStrategy>();
                });

            var host = builder.Build();

            var serviceScope = host.Services.CreateScope();

            var services = serviceScope.ServiceProvider;

            try
            {
                var appRunnerService = services.GetRequiredService<AppRunner>();

                appRunnerService.StartApp();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex}");
            }
        }
  }
}
