using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StravaDiscordBot.ParticipantApi.Storage;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using StravaDiscordBot.ParticipantApi.StravaClient;

namespace StravaDiscordBot.ParticipantApi.Services
{
    public interface IStravaCredentialsService
    {
        Task<StravaCredentials> GetByStravaId(long stravaId);
        Task UpsertTokens(long stravaId, StravaOauthResponse stravaOauthResponse);
    }
    public class StravaCredentialsService : IStravaCredentialsService
    {
        private readonly ParticipantContext _dbContext;

        public StravaCredentialsService(ParticipantContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<StravaCredentials> GetByStravaId(long stravaId)
        {
            return _dbContext.Credentials.FirstOrDefaultAsync(x => x.StravaId == stravaId);
        }

        public async Task UpsertTokens(long stravaId, StravaOauthResponse stravaOauthResponse)
        {
            var existing = await GetByStravaId(stravaId);
            if (existing == null)
                await _dbContext.Credentials.AddAsync(new StravaCredentials(stravaId, stravaOauthResponse.AccessToken, stravaOauthResponse.RefreshToken));
            else
            {
                existing.UpdateWithNewTokens(stravaOauthResponse);
                _dbContext.Credentials.Update(existing);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}