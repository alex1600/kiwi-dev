#region

using System;
using UnityEngine;

#endregion

namespace TwitchUnityBridge.Config
{
    [Serializable]
    public class TwitchConnectConfig
    {
        [SerializeField] private string username;
        [SerializeField] private string userToken;
        [SerializeField] private string channelName;

        public TwitchConnectConfig(string username, string userToken, string channelName)
        {
            this.username = username;
            if (userToken.StartsWith("oauth:"))
                this.userToken = userToken;
            else
                this.userToken = "oauth:" + userToken;
            this.channelName = channelName;
        }

        public string Username => username.ToLower();
        public string UserToken => userToken;
        public string ChannelName => channelName;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(UserToken) && !string.IsNullOrEmpty(ChannelName);
        }
    }
}
