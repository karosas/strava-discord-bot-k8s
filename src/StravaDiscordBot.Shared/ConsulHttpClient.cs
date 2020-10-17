using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Consul;
using Newtonsoft.Json;
using StravaDiscordBot.Shared.Exceptions;

namespace StravaDiscordBot.Shared
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string serviceName, string relativeUrl);
        Task<T> DeleteAsync<T>(string serviceName, string relativeUrl);
        Task<T> PostAsync<T>(string serviceName, string relativeUrl, object body = null);
    }

    public class ConsulHttpClient : IConsulHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConsulClient _consulClient;

        public ConsulHttpClient(HttpClient client, IConsulClient consulClient)
        {
            _client = client;
            _consulClient = consulClient;
        }

        public async Task<T> GetAsync<T>(string serviceName, string relativeUrl)
        {
            var uri = await GetRequestUriAsync(serviceName, relativeUrl);

            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new ConsulRequestException($"GET Request to service {serviceName}, relative url {relativeUrl} failed.", response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> DeleteAsync<T>(string serviceName, string relativeUrl)
        {
            var uri = await GetRequestUriAsync(serviceName, relativeUrl);

            var response = await _client.DeleteAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new ConsulRequestException($"GET Request to service {serviceName}, relative url {relativeUrl} failed.", response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> PostAsync<T>(string serviceName, string relativeUrl, object body = null)
        {
            var uri = await GetRequestUriAsync(serviceName, relativeUrl);

            var response = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(body)));
            
            if(!response.IsSuccessStatusCode)
                throw new ConsulRequestException($"POST Request to service {serviceName}, relative url {relativeUrl} failed.", response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        private async Task<Uri> GetRequestUriAsync(string serviceName, string relativeUrl)
        {
            var allRegistration = await _consulClient.Agent.Services();

            var matchingRegistrations = allRegistration.Response?
                .Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Value)
                .ToList();

            var service = GetRandomInstance(matchingRegistrations);

            if (service == null)
                throw new ConsulException($"Consul service: '{serviceName}' was not found.");

            var uriBuilder = new UriBuilder($"http://{service}{relativeUrl}")
            {
                Host = new Uri(service.Address).Host,
                Port = service.Port
            };

            return uriBuilder.Uri;
        }


        private static AgentService GetRandomInstance(IList<AgentService> services)
        {
            var random = new Random();

            var serviceToUse = services[random.Next(0, services.Count)];

            return serviceToUse;
        }
    }
}