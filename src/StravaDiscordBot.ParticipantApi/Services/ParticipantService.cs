using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StravaDiscordBot.ParticipantApi.Storage;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using StravaDiscordBot.ParticipantApi.StravaClient;

namespace StravaDiscordBot.ParticipantApi.Services
{
    // For data isolation, we pretty much always want leaderboardId to be provided to filter out other participants
    public interface IParticipantService
    {
        Task<Participant> GetOrDefault(ulong leaderboardId, ulong id);
        Task<Participant> GetByStravaOrDefault(ulong leaderboardId, long stravaId);
        Task<Participant> GetByStravaOrDefault(long stravaId);
        Task<IList<Participant>> GetAll(ulong leaderboardId);
        Task<Participant> Create(ulong id, long stravaId, StravaOauthResponse authResponse, ulong leaderboardId);
        Task Delete(Participant participant);
    }
    public class ParticipantService : IParticipantService
    {
        private readonly ParticipantContext _dbContext;

        public ParticipantService(ParticipantContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Participant> GetOrDefault(ulong leaderboardId, ulong id)
        {
            return _dbContext
                .Participants
                .FirstOrDefaultAsync(x => x.Id == id && x.LeaderboardId == leaderboardId);
        }

        public Task<Participant> GetByStravaOrDefault(ulong leaderboardId, long stravaId)
        {
            return _dbContext
                .Participants
                .FirstOrDefaultAsync(x => x.StravaId == stravaId && x.LeaderboardId == leaderboardId);
        }

        public Task<Participant> GetByStravaOrDefault(long stravaId)
        {
            return _dbContext
                .Participants
                .FirstOrDefaultAsync(x => x.StravaId == stravaId);
        }

        public async Task<IList<Participant>> GetAll(ulong leaderboardId)
        {
            var participants = await _dbContext
                .Participants
                .ToListAsync();

            return participants
                .Where(x => x.LeaderboardId == leaderboardId)
                .ToList();
        }

        public async Task<Participant> Create(ulong id, long stravaId, StravaOauthResponse authResponse, ulong leaderboardId)
        {
            var participant = new Participant
            {
                Id = id,
                StravaId = stravaId,
                LeaderboardId = leaderboardId
            };
            await _dbContext.Participants.AddAsync(participant);

            
            await _dbContext.SaveChangesAsync();
            return participant;
        }

        public async Task Delete(Participant participant)
        {
            var credentials = await _dbContext.Credentials.FirstOrDefaultAsync(x => x.StravaId == participant.StravaId);
            if (credentials != null)
                _dbContext.Credentials.Remove(credentials);
            _dbContext.Participants.Remove(participant);
            await _dbContext.SaveChangesAsync();
        }
    }
}