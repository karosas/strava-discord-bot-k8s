namespace StravaDiscordBot.Workers.Helpers
{
    public static class Formatters
    {
        public static string PlaceToEmote(int place)
        {
            return place switch
            {
                1 => "🥇",
                2 => "🥈",
                3 => "🥉",
                _ => place.ToString()
            };
        }
    }
}