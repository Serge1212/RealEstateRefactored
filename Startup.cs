using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealEstateRefactored.Interfaces;
using RealEstateRefactored.Services;

namespace RealEstateRefactored {
  public class Startup {
        static void Main(string[] args)
        {
            ConfigureServices();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(AppRunner.CurrentDomain_ProcessExit);
        }

        private static void ConfigureServices()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(config => config.AddConsole());
                    services.AddScoped<AppRunner>();
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
