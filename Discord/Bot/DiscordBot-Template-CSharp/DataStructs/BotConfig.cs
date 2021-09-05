namespace DiscordBotHumEncore.DataStructs
{
    public class BotConfig
    {
        //Bot
        public string Token { get; set; }
        public string Prefixe { get; set; }
        public string ReadyLog { get; set; }
        public string Join_message { get; set; }

        //GameStatus
        public string StreamUrl { get; set; }
        public string Currently { get; set; }
        public string Playing_status { get; set; }
        public string Status { get; set; }
    }
}
