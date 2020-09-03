namespace StravaDiscordBot.DiscordApi.Dto
{
    public class AthleteDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public bool Summit { get; set; }
        public string ProfileMedium { get; set; }
        public int FollowerCount { get; set; }
        public int Ftp { get; set; }
        public int FriendCount { get; set; }
        public float Weight { get; set; }
    }
}