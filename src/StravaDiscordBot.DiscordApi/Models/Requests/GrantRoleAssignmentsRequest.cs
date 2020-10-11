using System.Collections.Generic;

namespace StravaDiscordBot.DiscordApi.Models.Requests
{
    public class GrantRoleAssignmentsRequest
    {
        public string Name { get; set; }
        public IList<ulong> UserIds { get; set; }
    }
}