using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.ParticipantApi.Modules;
using StravaDiscordBot.ParticipantApi.Storage;
using StravaDiscordBot.Shared.Extensions;

namespace StravaDiscordBot.ParticipantApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ParticipantApiRootOptions>(Configuration);
            
            services.AddConsul(Configuration);
            services.AddDbContext<ParticipantContext>(ServiceLifetime.Singleton);
            
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new StravaClientModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsul();
            
            app.UseRouting();
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<ILogger<Startup>>()
                    .LogInformation("Ensuring database is created for ");

                var dbContext = serviceScope.ServiceProvider.GetService<ParticipantContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}