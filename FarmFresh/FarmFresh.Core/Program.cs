using Autofac.Extensions.DependencyInjection;
using Serilog;

namespace FarmFresh.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information($"Starting application...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Failed to start the application");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
