using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.DiscordApi.Dto;
using StravaDiscordBot.DiscordApi.Utilities;
using StravaDiscordBot.Shared;

namespace StravaDiscordBot.DiscordApi.DiscordControllers
{
    [Group("athlete")]
    public class AthleteDiscordController : ModuleBase<SocketCommandContext>
    {
        private readonly IConsulHttpClient _consulHttpClient;
        private readonly ILogger<AthleteDiscordController> _logger;

        public AthleteDiscordController(IConsulHttpClient consulHttpClient, ILogger<AthleteDiscordController> logger)
        {
            _consulHttpClient = consulHttpClient;
            _logger = logger;
        }

        [Command("")]
        [Summary("Get your strava athlete information")]
        [RequireToBeWhitelistedServer]
        public async Task GetLoggedInAthlete()
        {
            using (Context.Channel.EnterTypingState())
            {
                try
                {
                    var athlete = await _consulHttpClient.GetAsync<AthleteDto>(ServiceNames.StravaApi,
                        $"/v1/leaderboard/{Context.Guild.Id}/participant/{Context.User.Id}/athlete");

                    if (athlete == null)
                    {
                        await ReplyAsync("Couldn't find it.");
                        return;
                    }

                    await ReplyAsync(embed: BuildAthleteEmbed(athlete));
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Get logged in athlete failed");
                }
            }
        }

        [Command("")]
        [Summary("Get strava athlete of discord Id")]
        [RequireToBeWhitelistedServer]
        public async Task GetLoggedInAthlete(string discordId)
        {
            using (Context.Channel.EnterTypingState())
            {
                try
                {
                    var athlete = await _consulHttpClient.GetAsync<AthleteDto>(ServiceNames.StravaApi,
                        $"/v1/leaderboard/{Context.Guild.Id}/participant/{discordId}/athlete");

                    if (athlete == null)
                    {
                        await ReplyAsync("Couldn't find it.");
                        return;
                    }

                    await ReplyAsync(embed: BuildAthleteEmbed(athlete));
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Get athlete by id failed");
                }
            }
        }

        private static Embed BuildAthleteEmbed(AthleteDto athlete)
        {
            var embedBuilder = new EmbedBuilder
            {
                Title = "Strava Athlete",
                ThumbnailUrl = athlete.ProfileMedium,
            };

            embedBuilder.AddField("Id", athlete.Id);
            embedBuilder.AddField("Name", athlete.FirstName);
            embedBuilder.AddField("Ftp", athlete.Ftp);
            embedBuilder.AddField("Weight", athlete.Weight);
            embedBuilder.AddField("Friends", athlete.FriendCount);
            embedBuilder.AddField("Followers", athlete.FollowerCount);
            embedBuilder.AddField("Strava Summit", athlete.Summit);

            return embedBuilder.Build();
        }
        
    }
}