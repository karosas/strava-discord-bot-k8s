using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using StravaDiscordBot.Workers.Clients.DiscordApi;
using StravaDiscordBot.Workers.Clients.DiscordApi.Models;
using StravaDiscordBot.Workers.Clients.LeaderboardApi;
using StravaDiscordBot.Workers.Constants;
using StravaDiscordBot.Workers.Helpers;

namespace StravaDiscordBot.Workers
{
    public class ScheduledLeaderboardWorker : BackgroundService
    {
        private static string Schedule => "0 8 * * 1";

        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;

        private readonly ILogger<ScheduledLeaderboardWorker> _logger;
        private readonly IStravaDiscordBotDiscordApi _discordApi;
        private readonly IStravaDiscordBotLeaderboardApi _leaderboardApi;

        public ScheduledLeaderboardWorker(ILogger<ScheduledLeaderboardWorker> logger,
            IStravaDiscordBotDiscordApi discordApi, IStravaDiscordBotLeaderboardApi leaderboardApi)
        {
            _logger = logger;
            _discordApi = discordApi;
            _leaderboardApi = leaderboardApi;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions {IncludingSeconds = false});
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (DateTime.Now > _nextRun)
                {
                    _logger.LogInformation($"Started {nameof(ScheduledLeaderboardWorker)} worker");
                    await DoWork();
                    _logger.LogInformation($"Finished {nameof(ScheduledLeaderboardWorker)} worker");
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                    _logger.LogInformation($"{nameof(ScheduledLeaderboardWorker)} next occurence {_nextRun:s}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // 1min polling interval
            } while (!stoppingToken.IsCancellationRequested);
        }

        private async Task DoWork()
        {
            var leaderboards = await _leaderboardApi.GetAllLeaderboardsAsync();

            foreach (var leaderboard in leaderboards)
            {
                await _discordApi.RemoveAllInstancesOfRoleWithHttpMessagesAsync(leaderboard.Id.ToString(),
                    WinnerRole.Name);

                var startDate = DateTime.Now.AddDays(-7);

                var leaderboardResultViewModel =
                    await _leaderboardApi.GenerateLeaderboardResultsAsync(leaderboard.Id.ToString(),
                        start: startDate);

                foreach (var categoryResult in leaderboardResultViewModel.CategoryResults)
                {
                    var request = new SendEmbedMessageRequest
                    {
                        Title = categoryResult.Name,
                        Fields = new List<FieldViewModel>()
                    };

                    var place = 1;
                    categoryResult.OrderedParticipantResults.Take(3).ToList().ForEach(x =>
                    {
                        request.Fields.Add(new FieldViewModel(
                            $"{Formatters.PlaceToEmote(place)} - {x.DisplayValue}", $"<@{x.Participant.Id}>", true));
                        place++;
                    });

                    await _discordApi.SendEmbedMessageToChannelAsync(leaderboard.Id.ToString(),
                        leaderboard.ChannelId.ToString(), request);
                }

                var grantRoleRequest = new GrantRoleAssignmentsRequest
                {
                    Name = WinnerRole.Name,
                    UserIds = leaderboardResultViewModel.CategoryResults.SelectMany(x => x.OrderedParticipantResults)
                        .Select(x => x.Participant.Id).ToList()
                };

                await _discordApi.GrantRoleAssignmentsWithHttpMessagesAsync(leaderboard.Id.ToString(), grantRoleRequest);
            }
        }
    }
}