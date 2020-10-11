using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using StravaDiscordBot.Shared;
using StravaDiscordBot.Workers.Clients.DiscordApi.Models;
using StravaDiscordBot.Workers.Clients.LeaderboardApi.Models;
using StravaDiscordBot.Workers.Constants;
using StravaDiscordBot.Workers.Helpers;
using ServiceNames = StravaDiscordBot.Workers.Constants.ServiceNames;

namespace StravaDiscordBot.Workers
{
    public class ScheduledLeaderboardWorker : BackgroundService
    {
        private static string Schedule => "0 8 * * 1";

        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;

        private readonly ILogger<ScheduledLeaderboardWorker> _logger;
        private readonly IConsulHttpClient _consulHttpClient;

        public ScheduledLeaderboardWorker(ILogger<ScheduledLeaderboardWorker> logger,
            IConsulHttpClient consulHttpClient)
        {
            _logger = logger;
            _consulHttpClient = consulHttpClient;
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
            var leaderboards =
                await _consulHttpClient.GetAsync<IList<LeaderboardViewModel>>(ServiceNames.Leaderboard,
                    "v1/leaderboard");

            foreach (var leaderboard in leaderboards)
            {
                await _consulHttpClient.DeleteAsync<object>(ServiceNames.Discord,
                    $"/v1/server/{leaderboard.Id}/role/{WebUtility.UrlEncode(WinnerRole.Name)}/assignments");

                var startDate = DateTime.Now.AddDays(-7);

                var leaderboardResultViewModel =
                    await _consulHttpClient.GetAsync<LeaderboardResultViewModel>(ServiceNames.Leaderboard,
                        $"/v1/leaderboard/{leaderboard.Id}/result?start={startDate}");

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

                    await _consulHttpClient.PostAsync<object>(ServiceNames.Discord,
                        $"/v1/server/{leaderboard.Id}/channel/{leaderboard.ChannelId}/embed", request);
                }
                
                var grantRoleRequest = new GrantRoleAssignmentsRequest
                {
                    Name = WinnerRole.Name,
                    UserIds = leaderboardResultViewModel.CategoryResults.SelectMany(x => x.OrderedParticipantResults).Select(x => x.Participant.Id).ToList()
                };

                await _consulHttpClient.PostAsync<object>(ServiceNames.Discord,
                    $"/v1/server/{leaderboard.Id}/role/assignments", grantRoleRequest);
            }
        }
    }
}