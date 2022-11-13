using Autofac.Extensions.DependencyInjection;
using FarmFresh.Core.Services.Concrete;
using FarmFresh.Framework.Services.Abstract;
using Serilog;

namespace FarmFresh.Core
{
    public class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information($"Starting application...");

                var host = CreateHostBuilder(args).Build();

                ServiceProvider = host.Services;
                await InitializeSeedData();

                host.Run();
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


        //Initialize seeder
        public static async Task InitializeSeedData()
        {
            var dataSeederService = new DataSeederService(ServiceProvider.GetRequiredService<IUserService>());

            await dataSeederService.SeedUserData();
        }
    }
}
