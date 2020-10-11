using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StravaDiscordBot.Shared.Extensions;

namespace StravaDiscordBot.Workers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsul(Configuration);
        }
    }
}