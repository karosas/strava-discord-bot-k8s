using Autofac;
using StravaDiscordBot.ParticipantApi.Services;
using StravaDiscordBot.ParticipantApi.Storage.Entities;

namespace StravaDiscordBot.ParticipantApi.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActivityService>().As<IActivityService>().SingleInstance();
            builder.RegisterType<AthleteService>().As<IAthleteService>().SingleInstance();
            builder.RegisterType<DiscordService>().As<IDiscordService>().SingleInstance();
            builder.RegisterType<ParticipantService>().As<IParticipantService>().SingleInstance();
            builder.RegisterType<StravaAuthenticationService>().As<IStravaAuthenticationService>().SingleInstance();
            builder.RegisterType<StravaCredentialsService>().As<IStravaCredentialsService>().SingleInstance();
        }
    }
}