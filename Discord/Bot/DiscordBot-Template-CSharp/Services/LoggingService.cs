using System;
using Discord;
using System.Threading.Tasks;

namespace DiscordBotHumEncore.Services
{
    class LoggingService
    {
        public static async Task LogAsync(string src, LogSeverity severity, string message, Exception exception = null)
        {
            if (severity.Equals(null))
            {
                severity = LogSeverity.Warning;
            }
            await Append($"{DateTime.Now.ToString("dd/MM/yyyy h:mm tt"),-19}| {GetSeverityString(severity)}", GetConsoleColor(severity)); // 15/06/2021 8:45 AM | INFO [DISCD] Discord.Net v2.4.0 (API v6)
            //await Append($"{DateTime.Now.ToString("HH:mm:ss"),-19}| {GetSeverityString(severity)}", GetConsoleColor(severity)); // 8:45:06 | INFO [DISCD] Discord.Net v2.4.0 (API v6)
            // https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
            await Append($" [{SourceToString(src)}] ", ConsoleColor.DarkGray);

            if (!string.IsNullOrWhiteSpace(message))
                await Append($"{message}\n", ConsoleColor.White);
            else if (exception == null)
            {
                await Append("Unknown Exception. Exception Returned Null.\n", ConsoleColor.DarkRed);
            }
            else if (exception.Message == null)
                await Append($"Unknown \n{exception.StackTrace}\n", GetConsoleColor(severity));
            else
                await Append($"{exception.Message ?? "Unknown"}\n{exception.StackTrace ?? "Unknown"}\n", GetConsoleColor(severity));
        }

        public static async Task LogCriticalAsync(string source, string message, Exception exc = null)
            => await LogAsync(source, LogSeverity.Critical, message, exc);

        public static async Task LogInformationAsync(string source, string message)
            => await LogAsync(source, LogSeverity.Info, message);

        public static async Task LogSimpleAsync(string source, string message)
            => await LogAsync(source, LogSeverity.Info, message);

        private static async Task Append(string message, ConsoleColor color)
        {
            await Task.Run(() =>
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            });
        }

        private static string SourceToString(string src)
        {
            switch (src.ToLower())
            {
                case "discord":
                    return "DISCD";

                case "rest":
                    return "REST";

                case "audio":
                    return "AUDIO";

                case "admin":
                    return "ADMIN";

                case "gateway":
                    return "GTWAY";

                case "blacklist":
                    return "BLAKL";

                case "bot":
                    return "BOT";

                default:
                    return src;
            }
        }

        private static string GetSeverityString(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return "CRIT";

                case LogSeverity.Debug:
                    return "DBUG";

                case LogSeverity.Error:
                    return "EROR";

                case LogSeverity.Info:
                    return "INFO";

                case LogSeverity.Verbose:
                    return "VERB";

                case LogSeverity.Warning:
                    return "WARN";

                default: return "UNKN";
            }
        }

        private static ConsoleColor GetConsoleColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return ConsoleColor.Red;

                case LogSeverity.Debug:
                    return ConsoleColor.Magenta;

                case LogSeverity.Error:
                    return ConsoleColor.DarkRed;

                case LogSeverity.Info:
                    return ConsoleColor.Green;

                case LogSeverity.Verbose:
                    return ConsoleColor.DarkCyan;

                case LogSeverity.Warning:
                    return ConsoleColor.Yellow;

                default: return ConsoleColor.White;
            }
        }
    }
}
