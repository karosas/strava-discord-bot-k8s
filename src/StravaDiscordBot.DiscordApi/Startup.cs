using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Autofac;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using StravaDiscordBot.DiscordApi.Clients.LeaderboardApi;
using StravaDiscordBot.DiscordApi.Clients.ParticipantApi;
using StravaDiscordBot.DiscordApi.Health;
using StravaDiscordBot.DiscordApi.Modules;
using StravaDiscordBot.DiscordApi.Services;

namespace StravaDiscordBot.DiscordApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private DiscordSocketClient _discordSocketClient;
        private CommandHandlingService _commandHandlingService;
        private ILogger<Startup> _logger;

        public void ConfigureServices(IServiceCollection services)
        {
            var options = new DiscordRootOptions();
            Configuration.Bind(options);

            services.AddLogging(builder => builder.AddSerilog(dispose: true));
            services.Configure<DiscordRootOptions>(Configuration);
            services.AddHealthChecks()
                .AddCheck("leaderboard-api",
                    new UrlHealthCheck(new Uri($"{options.Consul.LeaderboardBaseUrl}/health/liveness")),
                    tags: new[] {"live"})
                .AddCheck("participant-api",
                    new UrlHealthCheck(new Uri($"{options.Consul.ParticipantBaseUrl}/health/liveness")),
                    tags: new[] {"live"});

            services.AddSingleton<IStravaDiscordBotLeaderboardApi>(
                new StravaDiscordBotLeaderboardApi(new Uri(options.Consul.LeaderboardBaseUrl)));
            services.AddSingleton<IStravaDiscordBotParticipantApi>(
                new StravaDiscordBotParticipantApi(new Uri(options.Consul.ParticipantBaseUrl)));
            
            services.AddControllers()
                .AddJsonOptions(jsonOpts =>
                    jsonOpts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            services.AddSwaggerGen();
            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DiscordModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseConsul();
            app.UseRouting();

            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaderboardAPI v1"); });

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

            StartDiscordBot(app, logger)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        private async Task StartDiscordBot(IApplicationBuilder app, ILogger<Startup> logger)
        {
            _logger = logger;

            var options = app.ApplicationServices.GetRequiredService<IOptionsMonitor<DiscordRootOptions>>();
            _discordSocketClient = app.ApplicationServices.GetRequiredService<DiscordSocketClient>();
            _discordSocketClient.Log += LogAsync;
            app.ApplicationServices.GetRequiredService<CommandService>().Log += LogAsync;
            await _discordSocketClient.LoginAsync(TokenType.Bot, options.CurrentValue.Discord.Token);
            await _discordSocketClient.StartAsync();

            _commandHandlingService = app.ApplicationServices.GetRequiredService<CommandHandlingService>();
            await _commandHandlingService.InstallCommandsAsync();
        }

        private Task LogAsync(LogMessage log)
        {
            var logLevel = log.Severity switch
            {
                LogSeverity.Critical => LogLevel.Critical,
                LogSeverity.Debug => LogLevel.Debug,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Verbose => LogLevel.Trace,
                _ => LogLevel.None
            };
            _logger.Log(logLevel, log.Exception, "[{discord_log_severity}] {discord_log_message}", log.Severity,
                log.Message);
            return Task.CompletedTask;
        }
    }
}