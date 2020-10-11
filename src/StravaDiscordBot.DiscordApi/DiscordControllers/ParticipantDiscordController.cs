using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using StravaDiscordBot.DiscordApi.Utilities;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using StravaDiscordBot.Shared;

namespace StravaDiscordBot.DiscordApi.DiscordControllers
{
    // TODO: Not sure if controller is confusing naming
    // For me these feel like controllers but for discord
    // Their base class is ModuleBase, but module for me associates with autofac
    [Group("participant")]
    public class ParticipantDiscordController : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ParticipantDiscordController> _logger;
        private readonly IConsulHttpClient _consulHttpClient;

        public ParticipantDiscordController(ILogger<ParticipantDiscordController> logger,
            IConsulHttpClient consulHttpClient)
        {
            _logger = logger;
            _consulHttpClient = consulHttpClient;
        }

        [Command("list")]
        [Summary("[ADMIN] List participants for server")]
        [RequireToBeWhitelistedServer]
        [RequireRole(new[] {"Owner", "Bot Manager"})]
        public async Task ListParticipants()
        {
            using (Context.Channel.EnterTypingState())
            {
                _logger.LogInformation("Executing list participants command");
                var participants = await _consulHttpClient.GetAsync<IList<Participant>>(ServiceNames.ParticipantApi,
                    $"/v1/leaderboard/{Context.Guild.Id}/participant");

                if (!participants.Any())
                    await ReplyAsync("Seems like there are no participants yet.");

                // Embeds allow max 25 fields. We want to show 2 fields per participant,
                // so we split participants into groups of 12 (24 fields)
                var participantSublists = participants
                    .Select((x, i) => new {Index = i, Value = x})
                    .GroupBy(x => x.Index / 12)
                    .Select(x => x.Select(v => v.Value).ToList())
                    .ToList();

                foreach (var participantSublist in participantSublists)
                {
                    var embedBuilder = new EmbedBuilder();
                    foreach (var participantDto in participantSublist)
                        embedBuilder.AddField("DiscordId", participantDto.Id, true);

                    await ReplyAsync(embed: embedBuilder.Build());
                }
            }
        }

        [Command("remove")]
        [Summary("[ADMIN] Remove participant from leaderboard by discord id")]
        [RequireToBeWhitelistedServer]
        [RequireRole(new[] {"Owner", "Bot Manager"})]
        public async Task RemoveParticipant(ulong discordId)
        {
            using (Context.Channel.EnterTypingState())
            {
                await _consulHttpClient.DeleteAsync<object>(
                    ServiceNames.ParticipantApi,
                    $"/v1/leaderboard/{Context.Guild.Id}/participant/{discordId}"
                    );

                await ReplyAsync("👍");
            }
        }
    }
}