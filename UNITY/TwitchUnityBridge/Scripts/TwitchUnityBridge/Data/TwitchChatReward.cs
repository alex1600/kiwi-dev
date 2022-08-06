namespace TwitchUnityBridge.Data
{
    public class TwitchChatReward : TwitchChatMessage
    {

        public TwitchChatReward(TwitchUser user, string message, string customRewardId) : base(user, message, 0)
        {
            CustomRewardId = customRewardId;
        }
        public string CustomRewardId { get; }
    }
}
