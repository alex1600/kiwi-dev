using System;
using UnityEngine;
using UnityEngine.Events;

namespace DiscordPresence
{
    [Serializable]
    public class DiscordJoinEvent : UnityEvent<string> { }

    [Serializable]
    public class DiscordSpectateEvent : UnityEvent<string> { }

    [Serializable]
    public class DiscordJoinRequestEvent : UnityEvent<DiscordRpc.JoinRequest> { }

    public class PresenceManager : MonoBehaviour
    {
        public DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence();
        public string applicationId;
        public string optionalSteamId;
        public int callbackCalls;
        public int clickCounter;
        public DiscordRpc.JoinRequest joinRequest;
        public UnityEvent onConnect;
        public UnityEvent onDisconnect;
        public UnityEvent hasResponded;
        public DiscordJoinEvent onJoin;
        public DiscordJoinEvent onSpectate;
        public DiscordJoinRequestEvent onJoinRequest;

        DiscordRpc.EventHandlers handlers;

        public static PresenceManager instance;

        public void OnClick()
        {
            #if UNITY_EDITOR
            Debug.Log("Discord: on click!");
            #endif
            clickCounter++;

            presence.details = string.Format("Button clicked {0} times", clickCounter);

           DiscordRpc.UpdatePresence(presence);
        }

        public void RequestRespondYes()
        {
        #if UNITY_EDITOR
            Debug.Log("Discord: responding yes to Ask to Join request");
            #endif
            DiscordRpc.Respond(joinRequest.userId, DiscordRpc.Reply.Yes);
            hasResponded.Invoke();
        }

        public void RequestRespondNo()
        {
        #if UNITY_EDITOR
            Debug.Log("Discord: responding no to Ask to Join request");
            #endif
            DiscordRpc.Respond(joinRequest.userId, DiscordRpc.Reply.No);
            hasResponded.Invoke();
        }

        #region Discord Callbacks
        public void ReadyCallback()
        {
            callbackCalls++;
            #if UNITY_EDITOR
            Debug.Log("Discord: ready");
            #endif
            onConnect.Invoke();
            UpdatePresence(null);
        }

        public void DisconnectedCallback(int errorCode, string message)
        {
            callbackCalls++;
            #if UNITY_EDITOR
            Debug.Log(string.Format("Discord: disconnect {0}: {1}", errorCode, message));
            #endif
            onDisconnect.Invoke();
        }

        public void ErrorCallback(int errorCode, string message)
        {
            callbackCalls++;
            #if UNITY_EDITOR
            Debug.Log(string.Format("Discord: error {0}: {1}", errorCode, message));
            #endif
        }

        public void JoinCallback(string secret)
        {
            callbackCalls++;
            #if UNITY_EDITOR
            Debug.Log(string.Format("Discord: join ({0})", secret));
            #endif
            onJoin.Invoke(secret);
        }

        public void SpectateCallback(string secret)
        {
            callbackCalls++;
            #if UNITY_EDITOR
            Debug.Log(string.Format("Discord: spectate ({0})", secret));
            #endif
            onSpectate.Invoke(secret);
        }

        public void RequestCallback(ref DiscordRpc.JoinRequest request)
        {
            callbackCalls++;
            #if UNITY_EDITOR
            Debug.Log(string.Format("Discord: join request {0}#{1}: {2}", request.username, request.discriminator, request.userId));
            #endif
            joinRequest = request;
            onJoinRequest.Invoke(request);
        }
        #endregion

        #region Monobehaviour Callbacks
        // Singleton
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            DiscordRpc.RunCallbacks();
        }

        void OnEnable()
        {
        #if UNITY_EDITOR
            Debug.Log("Discord: init");
            #endif
            callbackCalls = 0;

            handlers = new DiscordRpc.EventHandlers();
            handlers.readyCallback += ReadyCallback;
            handlers.disconnectedCallback += DisconnectedCallback;
            handlers.errorCallback += ErrorCallback;
            handlers.joinCallback += JoinCallback;
            handlers.spectateCallback += SpectateCallback;
            handlers.requestCallback += RequestCallback;
            DiscordRpc.Initialize(applicationId, ref handlers, true, optionalSteamId);
        }

        void OnDisable()
        {
        #if UNITY_EDITOR
            Debug.Log("Discord: shutdown");
            #endif
            DiscordRpc.Shutdown();
        }

        void OnDestroy()
        {

        }
        #endregion

        #region Update Presence Method
        public static void UpdatePresence(string detail, string state = null, long start = -1, long end = -1, string largeKey = null,string largeText = null, 
            string smallKey = null, string smallText = null, string partyId = null, int size = -1, int max = -1, string match = null, string join = null, 
            string spectate = null)
        {
            instance.presence.details = detail ?? instance.presence.details;
            instance.presence.state = state ?? instance.presence.state;
            instance.presence.startTimestamp = (start == -1) ? instance.presence.startTimestamp : start;
            instance.presence.endTimestamp = (end == -1) ? instance.presence.endTimestamp : end;
            instance.presence.largeImageKey = largeKey ?? instance.presence.largeImageKey;
            instance.presence.largeImageText = largeText ?? instance.presence.largeImageText;
            instance.presence.smallImageKey = smallKey ?? instance.presence.smallImageKey;
            instance.presence.smallImageText = smallText ?? instance.presence.smallImageText;
            instance.presence.partyId = partyId ?? instance.presence.partyId;
            instance.presence.partySize = (size == -1) ? instance.presence.partySize : size;
            instance.presence.partyMax = (max == -1) ? instance.presence.partyMax : max;
            instance.presence.matchSecret = match ?? instance.presence.matchSecret;
            instance.presence.joinSecret = join ?? instance.presence.joinSecret;
            instance.presence.spectateSecret = spectate ?? instance.presence.spectateSecret;
            DiscordRpc.UpdatePresence(instance.presence);
        }

        public static void ClearPresence()
        {
            instance.presence.details = "";
            instance.presence.state = "";
            instance.presence.startTimestamp = 0;
            instance.presence.endTimestamp = 0;
            instance.presence.largeImageKey = "";
            instance.presence.largeImageText = "";
            instance.presence.smallImageText = "";
            instance.presence.smallImageKey = "";
            instance.presence.partyId = "";
            instance.presence.partySize = 0;
            instance.presence.partyMax = 0;
            instance.presence.matchSecret = "";
            instance.presence.joinSecret = "";
            instance.presence.spectateSecret = "";
        }

        public static void ClearAndUpdate()
        {
            ClearPresence();
            DiscordRpc.UpdatePresence(instance.presence);
        }

        public static void SimpleUpdate() {
            DiscordRpc.UpdatePresence(instance.presence);
        }
        #endregion
    }
}
