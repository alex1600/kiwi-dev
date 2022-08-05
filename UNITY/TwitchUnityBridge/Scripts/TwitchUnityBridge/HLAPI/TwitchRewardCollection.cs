#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace TwitchUnityBridge.HLAPI
{
    public class TwitchRewardCollection : MonoBehaviour
    {

        [SerializeField] private List<TwitchRewardData> _rewards;
        public static TwitchRewardCollection instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool TryFind(string id, out TwitchRewardData data)
        {
            data = Find(id);
            return data != null;
        }

        public TwitchRewardData Find(string id)
        {
            foreach (TwitchRewardData reward in _rewards)
            {
                if (reward.TwitchId == id)
                    return reward;
            }
            return default;
        }
    }
}
