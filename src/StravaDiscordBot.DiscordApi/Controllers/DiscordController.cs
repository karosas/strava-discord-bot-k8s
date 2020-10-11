using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.AspNetCore.Mvc;
using StravaDiscordBot.DiscordApi.Models.Requests;

namespace StravaDiscordBot.DiscordApi.Controllers
{
    [ApiController]
    [Route("v1/")]
    public class DiscordController : ControllerBase
    {
        private readonly DiscordSocketClient _discordSocketClient;

        public DiscordController(DiscordSocketClient discordSocketClient)
        {
            _discordSocketClient = discordSocketClient;
        }

        [HttpPost("server/{serverId}/channel/{channelId}/text", Name = "SendTextMessageToChannel")]
        public async Task<ActionResult> SendTextMessageToChannel(ulong serverId, ulong channelId, SendTextMessageRequest message)
        {
            var guild = _discordSocketClient.GetGuild(serverId);
            if (guild == null)
                return NotFound();

            var channel = guild.GetTextChannel(channelId);
            if (channel == null)
                return NotFound();

            await channel.SendMessageAsync(message.Text);
            return Ok();
        }
        
        [HttpPost("server/{serverId}/channel/{channelId}/embed", Name = "SendEmbedMessageToChannel")]
        public async Task<ActionResult> SendTextMessageToChannel(ulong serverId, ulong channelId, SendEmbedMessageRequest request)
        {
            var guild = _discordSocketClient.GetGuild(serverId);
            if (guild == null)
                return NotFound();

            var channel = guild.GetTextChannel(channelId);
            if (channel == null)
                return NotFound();

            var embedBuilder = new EmbedBuilder()
                .WithTitle(request.Title);

            request.Fields.ForEach(x => embedBuilder.AddField(x.Name, x.Value, x.Inline));
            await channel.SendMessageAsync(embed: embedBuilder.Build());
         
            
            return Ok();
        }

        [HttpDelete("server/{serverId}/role/{roleName}/assignments", Name = "RemoveAllInstancesOfRole")]
        public async Task<ActionResult> RemoveAllRoleAssignments(ulong serverId, string roleName)
        {
            var guild = _discordSocketClient.GetGuild(serverId);
            if (guild == null)
                return NotFound();

            var role = guild.Roles.FirstOrDefault(x => x.Name == roleName);
            if (role == null)
                return NotFound();

            var usersWithRole = guild.Users
                .Where(x => x.Roles.Any(x => x.Name == roleName));

            
            foreach (var user in usersWithRole)
                await user.RemoveRoleAsync(role);

            return Ok();
        }

        [HttpPost("server/{serverId}/role/assignments", Name = "GrantRoleAssignments")]
        public async Task<ActionResult> GrantRoleAssignments(ulong serverId, GrantRoleAssignmentsRequest request)
        {
            var guild = _discordSocketClient.GetGuild(serverId);
            if (guild == null)
                return NotFound();

            var role = guild.Roles.FirstOrDefault(x => x.Name == request.Name);
            if (role == null)
                return NotFound();

            var users = guild.Users.Where(x => request.UserIds.Contains(x.Id));

            foreach (var user in users)
                await user.AddRoleAsync(role);

            return Ok();
        }
        
        [HttpPost("user/text", Name = "SendDM")]
        public async Task<ActionResult> SendDM(ulong userId, SendTextMessageRequest message)
        {
            var dmChannel = await _discordSocketClient.GetDMChannelAsync(userId);
            if (dmChannel == null)
                return NotFound();

            await dmChannel.SendMessageAsync(message.Text);
            return Ok();
        }
    }
}