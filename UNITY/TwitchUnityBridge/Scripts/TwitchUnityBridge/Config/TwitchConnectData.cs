#region

using UnityEngine;

#endregion

namespace TwitchUnityBridge.Config
{
    [CreateAssetMenu(fileName = "TwitchConnectData", menuName = "TwitchUnityBridge/TwitchConnectData", order = 1)]
    public class TwitchConnectData : ScriptableObject
    {
        [SerializeField] private TwitchConnectConfig twitchConnectConfig;
        public TwitchConnectConfig TwitchConnectConfig => twitchConnectConfig;
    }
}
