using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace StravaDiscordBot.Shared.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static string UseConsul(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var consulConfig = scope.ServiceProvider.GetRequiredService<IOptions<ConsulOptions>>();

            var serviceId = Guid.NewGuid();
            var consulServiceId = $"{consulConfig.Value.Service}:{serviceId}";

            var client = scope.ServiceProvider.GetService<IConsulClient>();

            var consulServiceRegistration = new AgentServiceRegistration
            {
                Name = consulConfig.Value.Service,
                ID = consulServiceId,
                Address = consulConfig.Value.ServiceAddress.ToString(),
                Port = consulConfig.Value.Port
            };

            client.Agent.ServiceRegister(consulServiceRegistration);

            return consulServiceId;
        }
    }
}