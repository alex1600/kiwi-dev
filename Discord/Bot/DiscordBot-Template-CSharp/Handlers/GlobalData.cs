using DiscordBotHumEncore.DataStructs;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotHumEncore.Handlers
{
        public class GlobalData
        {
            public static string ConfigPath { get; set; } = "Config.json";
            public static BotConfig Config { get; set; }

            public async Task InitializeAsync()
            {
                var json = string.Empty;

                if (!File.Exists(ConfigPath))
                {
                    json = JsonConvert.SerializeObject(GenerateNewConfig(), Formatting.Indented);
                    File.WriteAllText("Config.json", json, new UTF8Encoding(false));
                    await Task.Delay(-1);
                }

                json = File.ReadAllText(ConfigPath, new UTF8Encoding(false));
                Config = JsonConvert.DeserializeObject<BotConfig>(json);
            }

            private static BotConfig GenerateNewConfig() => new BotConfig
            {
                //BOT
                Token = "TOKEN HERE",
                Prefixe = "!",
                ReadyLog = "i'm ready!",
                Join_message = "CHANGE ME IN CONFIG",
                //GameStatus
                StreamUrl = "",
                Currently = "playing|listening|watching|streaming|custom",
                Playing_status = "CHANGE ME IN CONFIG",
                Status = "online|Invisible|DoNotDisturb|idle|offline|AFK",
            };
        }
}
