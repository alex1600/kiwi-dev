#region

using TwitchUnityBridge.Config;

#endregion

namespace TwitchUnityBridge.Client
{
    public class TwitchInputLine
    {

        public TwitchInputLine(string message, string prefix)
        {
            Type = TwitchInputType.UNKNOWN;
            Message = string.Empty;
            UserName = string.Empty;

            if (string.IsNullOrEmpty(message)) return;

            Message = message;
            if (message.StartsWith(TwitchChatRegex.LOGIN_SUCCESS_MESSAGE)) Type = TwitchInputType.LOGIN;
            else if (message.Contains(TwitchChatRegex.COMMAND_NOTICE)) Type = TwitchInputType.NOTICE;
            else if (message.Contains(TwitchChatRegex.COMMAND_PING)) Type = TwitchInputType.PING;
            else if (message.Contains(TwitchChatRegex.COMMAND_MESSAGE) && message.Contains(TwitchChatRegex.CUSTOM_REWARD_TEXT)) Type = TwitchInputType.MESSAGE_REWARD;
            else if (message.Contains(TwitchChatRegex.COMMAND_MESSAGE)) Type = IsCommandPrefix(prefix) ? TwitchInputType.MESSAGE_COMMAND : TwitchInputType.MESSAGE_CHAT;
            else if (message.Contains(TwitchChatRegex.COMMAND_JOIN))
            {
                Type = TwitchInputType.JOIN;
                UserName = TwitchChatRegex.JoinRegex.Match(Message).Groups[1].Value;
            }
            else if (message.Contains(TwitchChatRegex.COMMAND_PART))
            {
                Type = TwitchInputType.PART;
                UserName = TwitchChatRegex.PartRegex.Match(Message).Groups[1].Value;
            }
        }
        public TwitchInputType Type { get; }
        public string Message { get; }
        public string UserName { get; }

        public bool IsValidLogin(TwitchConnectConfig config)
        {
            if (Type != TwitchInputType.LOGIN) throw new("IsValidLogin can only be used in LOGIN type commands");
            return Message.StartsWith($"{TwitchChatRegex.LOGIN_SUCCESS_MESSAGE} {config.Username}");
        }

        public bool IsCommandPrefix(string prefix)
        {
            string payload = TwitchChatRegex.MessageRegex.Match(Message).Groups[5].Value;
            return payload.StartsWith(prefix);
        }
    }
}
