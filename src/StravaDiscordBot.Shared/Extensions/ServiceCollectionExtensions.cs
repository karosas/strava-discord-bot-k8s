using System;
using System.Linq;
using System.Net.Http;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StravaDiscordBot.Shared.Exceptions;

namespace StravaDiscordBot.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration config)
        {
            if (config.GetChildren().All(item => item.Key.ToLower() != "consul"))
                throw new ConsulException("'Consul' configuration section is required");

            var consulOptions = new ConsulOptions();
            config.GetSection("Consul").Bind(consulOptions);

            services.Configure<ConsulOptions>(config.GetSection("Consul"));
            services.AddHttpClient<IConsulHttpClient, ConsulHttpClient>();

            return services.AddSingleton<IConsulClient>(c => new ConsulClient(consulClientConfig =>
            {
                consulClientConfig.Address = consulOptions.ConsulAddress;
            }));
        }
    }
}