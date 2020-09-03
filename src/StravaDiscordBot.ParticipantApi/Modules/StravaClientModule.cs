using Autofac;
using StravaDiscordBot.ParticipantApi.StravaClient.Api;

namespace StravaDiscordBot.ParticipantApi.Modules
{
    public class StravaClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ActivitiesApi>().As<IActivitiesApi>();
            builder.RegisterType<AthletesApi>().As<IAthletesApi>();
        }
    }
}