#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace TwitchUnityBridge.Data
{
    public class TwitchUser
    {
        private static readonly string BADGE_SUB_NAME = "subscriber";
        private static readonly string BADGE_TURBO_NAME = "turbo";
        private static readonly string BADGE_PRIME_NAME = "premium";
        private static readonly string BADGE_VIP_NAME = "vip";
        private static readonly string BADGE_MODERATOR_NAME = "moderator";
        private static readonly string BADGE_BITS_LEADER_NAME = "bits-leader";
        private static readonly string BADGE_BROADCASTER_NAME = "broadcaster";
        private static readonly string BADGE_BITS_NAME = "bits";
        private static readonly string BADGE_FOUNDER_NAME = "founder";

        private string _displayName;

        public TwitchUser(string username)
        {
            Username = username;
        }

        public string Username { get; }
        public string Id { get; private set; }

        public string DisplayName => _displayName ?? Username;
        public IReadOnlyList<TwitchUserBadge> Badges { get; private set; }
        public bool IsSub => HasBadge(BADGE_SUB_NAME);
        public bool IsTurbo => HasBadge(BADGE_TURBO_NAME);
        public bool IsPrime => HasBadge(BADGE_PRIME_NAME);
        public bool IsVip => HasBadge(BADGE_VIP_NAME);
        public bool IsModerator => HasBadge(BADGE_MODERATOR_NAME);
        public bool IsBitsLeader => HasBadge(BADGE_BITS_LEADER_NAME);
        public bool IsBroadcaster => HasBadge(BADGE_BROADCASTER_NAME);
        public bool IsBits => HasBadge(BADGE_BITS_NAME);
        public bool IsFounder => HasBadge(BADGE_FOUNDER_NAME);

        public void SetData(string id, string displayName, IReadOnlyList<TwitchUserBadge> badges)
        {
            Id = id;
            Badges = badges;
            _displayName = displayName;
        }

        public bool HasBadge(string badgeName)
        {
            return Badges.Any(badge => badge.Name == badgeName);
        }

        public TwitchUserBadge GetBadge(string badgeName)
        {
            return Badges.Where(badge => badge.Name == badgeName).SingleOrDefault();
        }
    }
}
