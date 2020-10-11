using System.Collections.Generic;
using StravaDiscordBot.DiscordApi.Models.ViewModels;

namespace StravaDiscordBot.DiscordApi.Models.Requests
{
    public class SendEmbedMessageRequest
    {
        public List<FieldViewModel> Fields { get; set; }
        public string Title { get; set; }
    }
}