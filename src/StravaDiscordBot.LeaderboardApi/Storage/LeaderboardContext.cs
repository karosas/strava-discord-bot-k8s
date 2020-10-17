using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StravaDiscordBot.LeaderboardApi.Storage.Entities;

namespace StravaDiscordBot.LeaderboardApi.Storage
{
    public class LeaderboardContext : DbContext
    {
        private readonly IOptionsMonitor<LeaderboardApiRootOptions> _optionsMonitor;

        public LeaderboardContext(IOptionsMonitor<LeaderboardApiRootOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }
        
        public DbSet<Leaderboard> Leaderboards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_optionsMonitor.CurrentValue.ConnectionString);
        }
    }
}