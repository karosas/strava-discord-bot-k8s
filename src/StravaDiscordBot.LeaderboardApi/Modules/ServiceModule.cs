using Autofac;
using StravaDiscordBot.LeaderboardApi.Services;

namespace StravaDiscordBot.LeaderboardApi.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryService>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<LeaderboardService>().As<ILeaderboardService>().SingleInstance();
        }
    }
}