namespace TwitchUnityBridge.Data
{
    public class TwitchChatMessage
    {
        private static readonly string MESSAGE_HIGHLIGHTED = "highlighted-message";

        private readonly string _idMessage;
        public int Bits;

        public TwitchChatMessage(TwitchUser user, string message, int bits, string idMessage = "")
        {
            Message = message;
            User = user;
            Bits = bits;
            _idMessage = idMessage;
        }

        public TwitchUser User { get; }
        public string Message { get; }

        public bool IsHighlighted => _idMessage == MESSAGE_HIGHLIGHTED;
    }
}
