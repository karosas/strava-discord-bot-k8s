using System;
using System.Text.Json.Serialization;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.LeaderboardApi.Clients.ParticipantApi;
using StravaDiscordBot.LeaderboardApi.Modules;
using StravaDiscordBot.LeaderboardApi.Storage;

namespace StravaDiscordBot.LeaderboardApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var options = new LeaderboardApiRootOptions();
            Configuration.Bind(options);
            
            services.Configure<LeaderboardApiRootOptions>(Configuration);
            services.AddHealthChecks();
            
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<LeaderboardContext>(ServiceLifetime.Singleton);

            services.AddSingleton<IStravaDiscordBotParticipantApi>(
                new StravaDiscordBotParticipantApi(new Uri(options.Consul.ParticipantBaseUrl)));
            
            services.AddControllers()
                .AddJsonOptions(jsonOpts =>
                jsonOpts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddSwaggerGen();;
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaderboardAPI v1"); });

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

                var dbContext = serviceScope.ServiceProvider.GetService<LeaderboardContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains("startup")
                });
                
                endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains("readiness")
                });
                
                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains("liveness")
                });
                endpoints.MapControllers();
            });
        }
    }
}