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
        Task<T> GetAsync<T>(string serviceName, Uri requestUri);
        Task<T> PostAsync<T>(string serviceName, Uri requestUri, object body = null);
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

        public async Task<T> GetAsync<T>(string serviceName, Uri requestUri)
        {
            var uri = await GetRequestUriAsync(serviceName, requestUri);

            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new ConsulRequestException($"GET Request to service {serviceName}, url {requestUri} failed.", response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> PostAsync<T>(string serviceName, Uri requestUri, object body = null)
        {
            var uri = await GetRequestUriAsync(serviceName, requestUri);

            var response = await _client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(body)));
            
            if(!response.IsSuccessStatusCode)
                throw new ConsulRequestException($"POST Request to service {serviceName}, url {requestUri} failed.", response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        private async Task<Uri> GetRequestUriAsync(string serviceName, Uri uri)
        {
            var allRegistration = await _consulClient.Agent.Services();

            var matchingRegistrations = allRegistration.Response?
                .Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Value)
                .ToList();

            var service = GetRandomInstance(matchingRegistrations, serviceName);

            if (service == null)
                throw new ConsulException($"Consul service: '{serviceName}' was not found.");

            var uriBuilder = new UriBuilder(uri)
            {
                Host = service.Address,
                Port = service.Port
            };

            return uriBuilder.Uri;
        }


        private AgentService GetRandomInstance(IList<AgentService> services, string serviceName)
        {
            var random = new Random();

            var serviceToUse = services[random.Next(0, services.Count)];

            return serviceToUse;
        }
    }
}