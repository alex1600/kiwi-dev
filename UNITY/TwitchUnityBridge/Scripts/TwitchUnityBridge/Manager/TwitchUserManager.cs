#region

using System.Collections.Generic;
using System.Linq;
using TwitchUnityBridge.Data;

#endregion

namespace TwitchUnityBridge.Manager
{
    public static class TwitchUserManager
    {

        public delegate void UserAdded(TwitchUser twitchUser);

        public delegate void UserRemoved(string username);

        private static readonly Dictionary<string, TwitchUser> users = new Dictionary<string, TwitchUser>();

        public static UserAdded OnUserAdded;
        public static UserRemoved OnUserRemoved;

        public static List<TwitchUser> Users => users.Values.ToList();

        public static TwitchUser AddUser(string username)
        {
            var twitchUser = new TwitchUser(username);
            username = twitchUser.Username.ToUpper();
            if (HasUser(username)) return users[username];
            users.Add(username, twitchUser);
            OnUserAdded?.Invoke(twitchUser);
            return twitchUser;
        }

        public static void RemoveUser(string username)
        {
            username = username.ToUpper();
            if (!HasUser(username)) return;
            users.Remove(username);
            OnUserRemoved?.Invoke(username);
        }

        public static bool HasUser(string username)
        {
            username = username.ToUpper();
            return users.ContainsKey(username);
        }

        public static TwitchUser GetUser(string username)
        {
            if (!HasUser(username)) return AddUser(username);
            username = username.ToUpper();
            return users[username];
        }
    }
}
