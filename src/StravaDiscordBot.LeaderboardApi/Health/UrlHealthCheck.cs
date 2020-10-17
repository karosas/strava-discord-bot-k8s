using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace StravaDiscordBot.LeaderboardApi.Health
{
    public class UrlHealthCheck : IHealthCheck
    {
        private readonly Uri _url;
        private readonly int _lowAcceptedCode;
        private readonly int _highAcceptedCode;
        
        public UrlHealthCheck(Uri url, string acceptedStatusCodeRange = "200-299")
        {
            _url = url;
            if(!TryParseRange(acceptedStatusCodeRange, out _lowAcceptedCode, out _highAcceptedCode))
                throw new ArgumentException("Not a valid http status code range", nameof(acceptedStatusCodeRange));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            using var httpClient = new HttpClient();

            var result = await httpClient.GetAsync(_url, cancellationToken);
            
            if((int) result.StatusCode >= _lowAcceptedCode && (int) result.StatusCode <= _highAcceptedCode)
                return HealthCheckResult.Healthy();
            
            // TODO: Probably upgrade to unhealthy, not sure atm if this would cause circular health check dependency
            return HealthCheckResult.Degraded();
        }

        private static bool TryParseRange(string rangeString, out int lowAcceptedStatusCode, out int highAcceptedStatusCode)
        {
            lowAcceptedStatusCode = -1;
            highAcceptedStatusCode = -1;

            if (!rangeString.Contains('-'))
                return false;

            return int.TryParse(rangeString.Split("-")[0], out lowAcceptedStatusCode) &&
                   int.TryParse(rangeString.Split("-")[1], out highAcceptedStatusCode);
        }
    }
}