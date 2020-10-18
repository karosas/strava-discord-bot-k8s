using System;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StravaDiscordBot.Workers.Clients.DiscordApi;
using StravaDiscordBot.Workers.Clients.LeaderboardApi;
using StravaDiscordBot.Workers.Clients.ParticipantApi;

namespace StravaDiscordBot.Workers
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
            var options = new WorkerRootOptions();
            Configuration.Bind(options);
            
            services.Configure<WorkerRootOptions>(Configuration);
            services.AddHealthChecks();
            
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IStravaDiscordBotParticipantApi>(
                new StravaDiscordBotParticipantApi(new Uri(options.Consul.ParticipantBaseUrl)));

            services.AddSingleton<IStravaDiscordBotDiscordApi>(
                new StravaDiscordBotDiscordApi(new Uri(options.Consul.DiscordBaseUrl)));

            services.AddSingleton<IStravaDiscordBotLeaderboardApi>(
                new StravaDiscordBotLeaderboardApi(new Uri(options.Consul.LeaderboardBaseUrl)));
            
            services.AddHostedService<ScheduledLeaderboardWorker>();

            services
                .AddControllers()
                .AddJsonOptions(jsonOpts =>
                    jsonOpts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddSwaggerGen();
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkersApi v1"); });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

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