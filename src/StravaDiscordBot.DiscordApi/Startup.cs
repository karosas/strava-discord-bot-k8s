using System;
using System.Threading.Tasks;
using Autofac;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using StravaDiscordBot.DiscordApi.Modules;
using StravaDiscordBot.DiscordApi.Services;
using StravaDiscordBot.Shared.Extensions;

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
            services.AddLogging(builder => builder.AddSerilog(dispose: true));
            services.Configure<DiscordRootOptions>(Configuration);
            services.AddConsul(Configuration);
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DiscordModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseConsul();
            
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
            _logger.Log(logLevel, log.Exception, "[{discord_log_severity}] {discord_log_message}", log.Severity, log.Message);
            return Task.CompletedTask;
        }
    }
}