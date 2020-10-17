using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StravaDiscordBot.Workers.Clients.DiscordApi;
using StravaDiscordBot.Workers.Clients.LeaderboardApi;
using StravaDiscordBot.Workers.Clients.ParticipantApi;

namespace StravaDiscordBot.Workers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var options = new WorkerRootOptions();
                    hostContext.Configuration.Bind(options);

                    services.Configure<WorkerRootOptions>(hostContext.Configuration);

                    services.AddSingleton<IStravaDiscordBotParticipantApi>(
                        new StravaDiscordBotParticipantApi(new Uri(options.Consul.ParticipantBaseUrl)));

                    services.AddSingleton<IStravaDiscordBotDiscordApi>(
                        new StravaDiscordBotDiscordApi(new Uri(options.Consul.DiscordBaseUrl)));

                    services.AddSingleton<IStravaDiscordBotLeaderboardApi>(
                        new StravaDiscordBotLeaderboardApi(new Uri(options.Consul.LeaderboardBaseUrl)));
                    services.AddHostedService<ScheduledLeaderboardWorker>();
                });
    }
}