using Autofac;
using Discord.Commands;
using Discord.WebSocket;
using StravaDiscordBot.DiscordApi.DiscordControllers;
using StravaDiscordBot.DiscordApi.Services;

namespace StravaDiscordBot.DiscordApi.Modules
{
    public class DiscordModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<DiscordSocketClient>().SingleInstance();
            builder.RegisterType<CommandService>();
            builder.RegisterType<CommandHandlingService>();
            
            builder.RegisterType<ParticipantDiscordController>();
            builder.RegisterType<MetaDiscordController>();
            //builder.RegisterType<AthleteDiscordController>();
        }
    }
}