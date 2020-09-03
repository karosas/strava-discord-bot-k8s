using Microsoft.Extensions.Options;
using StravaDiscordBot.ParticipantApi.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace StravaDiscordBot.ParticipantApi.Storage
{
    public class ParticipantContext : DbContext
    {
        private readonly IOptionsMonitor<ParticipantApiRootOptions> _optionsMonitor;

        public ParticipantContext(IOptionsMonitor<ParticipantApiRootOptions> optionsMonitor)
        {
            _optionsMonitor = optionsMonitor;
        }
        
        public DbSet<Participant> Participants { get; set; }
        public DbSet<StravaCredentials> Credentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Sqlite stronk
            optionsBuilder.UseSqlite(_optionsMonitor.CurrentValue.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // To limit that a single discord user could sign up once in same discord server
            modelBuilder.Entity<Participant>()
                .HasKey(c => new {c.Id, c.LeaderboardId});
        }
    }
}