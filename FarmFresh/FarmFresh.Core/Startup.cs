using Autofac;
using Autofac.Extensions.DependencyInjection;
using FarmFresh.Core.Services.Abstract;
using FarmFresh.Core.Services.Concrete;
using FarmFresh.Framework;
using FarmFresh.Framework.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfigurationProvider = FarmFresh.Core.Providers.Concrete.ConfigurationProvider;
using IConfigurationProvider = FarmFresh.Core.Providers.Abstract.IConfigurationProvider;

namespace FarmFresh.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionString = new ConfigurationProvider().DatabaseConnectionString;
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;

            builder.RegisterModule(new FrameworkModule(connectionString, migrationAssemblyName));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = new ConfigurationProvider().DatabaseConnectionString;
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;

            services.AddControllers();
            services.AddSwaggerGen();

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });

            //Register Application Services 
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IConfigurationProvider, ConfigurationProvider>();

            services.AddDbContext<FrameworkContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssemblyName)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farm Fresh Api V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
