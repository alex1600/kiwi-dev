#region

using System.Collections;
using System.IO;
using System.Net.Sockets;
using TwitchUnityBridge.Config;
using TwitchUnityBridge.Data;
using TwitchUnityBridge.Manager;
using TwitchUnityBridge.Parser;
using UnityEngine;

#endregion

namespace TwitchUnityBridge.Client
{
    public class TwitchChatClient : MonoBehaviour
    {

        public delegate void OnChatCommandReceived(TwitchChatCommand chatCommand);

        public delegate void OnChatMessageReceived(TwitchChatMessage chatMessage);

        public delegate void OnChatRewardReceived(TwitchChatReward chatReward);

        public delegate void OnError(string errorMessage);

        public delegate void OnSuccess();

        public delegate void OnTwitchInputLine(TwitchInputLine twitchInput);

        private const string COMMAND_PONG = "PONG :tmi.twitch.tv";
        private const string LOGIN_FAILED_MESSAGE = "Login authentication failed";
        private const string LOGIN_WRONG_REQUEST_MESSAGE = "Wrong token format. It needs to start with 'oauth:'";
        private const string LOGIN_UNEXPECTED_ERROR_MESSAGE = "Unexpected error.";
        private const string LOGIN_WRONG_USERNAME = "The user token is correct but it does not belong to the given username.";
        [Header("Command prefix, by default is '!' (only 1 character)")] [SerializeField]
        private string _commandPrefix = "!";

        [Header("Optional init Twitch configuration")] [SerializeField]
        private TwitchConnectData _initTwitchConnectData;

        private bool _isAuthenticated;

        private OnError _onError;
        private OnSuccess _onSuccess;
        private StreamReader _reader;
        private TcpClient _twitchClient;

        private TwitchConnectConfig _twitchConnectConfig;
        private StreamWriter _writer;

        public string CommandPrefix => _commandPrefix;
        public OnChatMessageReceived onChatMessageReceived { get; set; }
        public OnChatCommandReceived onChatCommandReceived { get; set; }
        public OnChatRewardReceived onChatRewardReceived { get; set; }
        public OnTwitchInputLine onTwitchInputLine { get; set; }

        private void FixedUpdate()
        {
            if (!IsConnected()) return;
            ReadChatLine();
        }

        private void OnDestroy()
        {
            CloseTcpClient();
        }

        private void OnApplicationQuit()
        {
            CloseTcpClient();
        }

        public void Init(OnSuccess onSuccess, OnError onError)
        {
            Init(_initTwitchConnectData.TwitchConnectConfig, onSuccess, onError);
        }

        public void Init(TwitchConnectConfig twitchConnectConfig, OnSuccess onSuccess, OnError onError)
        {
            _twitchConnectConfig = twitchConnectConfig;

            if (IsConnected() && _isAuthenticated)
            {
                onSuccess();
                return;
            }

            // Checks
            if (_twitchConnectConfig == null || !_twitchConnectConfig.IsValid())
            {
                string errorMessage =
                    "TwitchChatClient.Init :: Twitch connect data is invalid, all fields are mandatory.";
                onError(errorMessage);
                return;
            }

            if (string.IsNullOrEmpty(_commandPrefix)) _commandPrefix = "!";

            if (_commandPrefix.Length > 1)
            {
                string errorMessage =
                    $"TwitchChatClient.Init :: Command prefix length should contain only 1 character. Command prefix: {_commandPrefix}";
                onError(errorMessage);
                return;
            }

            _onError = onError;
            _onSuccess = onSuccess;
            Login();
        }

        private void Login()
        {
            _twitchClient = new("irc.chat.twitch.tv", 6667);
            _reader = new(_twitchClient.GetStream());
            _writer = new(_twitchClient.GetStream());

            _writer.WriteLine($"PASS {_twitchConnectConfig.UserToken}");
            _writer.WriteLine($"NICK {_twitchConnectConfig.Username}");
            _writer.WriteLine($"JOIN #{_twitchConnectConfig.ChannelName}");

            _writer.WriteLine("CAP REQ :twitch.tv/tags");
            _writer.WriteLine("CAP REQ :twitch.tv/commands");
            _writer.WriteLine("CAP REQ :twitch.tv/membership");

            _writer.Flush();
        }

        private void CloseTcpClient()
        {
            if (_twitchClient == null) return;
            try
            {
                _twitchClient.Close();
                _twitchClient.Dispose();
                _twitchClient = null;
                Debug.Log("<color=orange>Twitch Disconnected</color>");
            }
            catch { }
        }

        private void ReadChatLine()
        {
            if (_twitchClient.Available <= 0) return;
            string source = _reader.ReadLine();
            var inputLine = new TwitchInputLine(source, _commandPrefix);
            onTwitchInputLine?.Invoke(inputLine);

            switch (inputLine.Type)
            {
                case TwitchInputType.LOGIN:
                    if (inputLine.IsValidLogin(_twitchConnectConfig))
                    {
                        _isAuthenticated = true;
                        _onSuccess?.Invoke();
                        _onSuccess = null;
                        Debug.Log("<color=green>!Success Twitch Connection!</color>");
                    }
                    else
                    {
                        _onError?.Invoke(LOGIN_WRONG_USERNAME);
                        _onError = null;
                        Debug.Log("<color=red>¡Error Twitch Connection: Token is valid but it belongs to another user!</color>");
                    }
                    break;

                case TwitchInputType.NOTICE:
                    string lineMessage = inputLine.Message;
                    string userErrorMessage;
                    string errorMessage;
                    if (lineMessage.Contains(TwitchChatRegex.LOGIN_FAILED_MESSAGE))
                    {
                        userErrorMessage = LOGIN_FAILED_MESSAGE;
                        errorMessage = LOGIN_FAILED_MESSAGE;
                    }
                    else if (lineMessage.Contains(TwitchChatRegex.LOGIN_WRONG_REQUEST_MESSAGE))
                    {
                        userErrorMessage = LOGIN_WRONG_REQUEST_MESSAGE;
                        errorMessage = LOGIN_WRONG_REQUEST_MESSAGE;
                    }
                    else
                    {
                        userErrorMessage = LOGIN_UNEXPECTED_ERROR_MESSAGE;
                        errorMessage = lineMessage;
                    }
                    _onError?.Invoke(userErrorMessage);
                    _onError = null;
                    Debug.Log($"<color=red>Twitch connection error: {errorMessage}</color>");
                    break;

                case TwitchInputType.PING:
                    _writer.WriteLine(COMMAND_PONG);
                    _writer.Flush();
                    break;

                case TwitchInputType.MESSAGE_COMMAND:
                {
                    var payload = new TwitchChatMessageParser(inputLine);
                    var chatCommand = new TwitchChatCommand(payload.User, payload.Sent, payload.Bits, payload.Id);
                    onChatCommandReceived?.Invoke(chatCommand);
                }
                    break;

                case TwitchInputType.MESSAGE_CHAT:
                {
                    var payload = new TwitchChatMessageParser(inputLine);
                    var chatMessage = new TwitchChatMessage(payload.User, payload.Sent, payload.Bits, payload.Id);
                    onChatMessageReceived?.Invoke(chatMessage);
                }
                    break;

                case TwitchInputType.MESSAGE_REWARD:
                {
                    var payload = new TwitchChatRewardParser(inputLine);
                    var chatReward = new TwitchChatReward(payload.User, payload.Sent, payload.Id);
                    onChatRewardReceived?.Invoke(chatReward);
                }
                    break;

                case TwitchInputType.JOIN:
                    TwitchUserManager.AddUser(inputLine.UserName);
                    break;

                case TwitchInputType.PART:
                    TwitchUserManager.RemoveUser(inputLine.UserName);
                    break;
            }
        }

        public bool SendChatMessage(string message)
        {
            if (!IsConnected()) return false;
            SendTwitchMessage(message);
            return true;
        }

        public bool SendChatMessage(string message, float seconds)
        {
            if (!IsConnected()) return false;
            StartCoroutine(SendTwitchChatMessageWithDelay(message, seconds));
            return true;
        }

        public void SendWhisper(string username, string message)
        {
            _writer.WriteLine($"PRIVMSG #jtv :/w {username} {message}");
            _writer.Flush();
        }

        private IEnumerator SendTwitchChatMessageWithDelay(string message, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            SendTwitchMessage(message);
        }

        private void SendTwitchMessage(string message)
        {
            _writer.WriteLine($"PRIVMSG #{_twitchConnectConfig.ChannelName} :/me {message}");
            _writer.Flush();
        }

        private bool IsConnected()
        {
            return _twitchClient != null && _twitchClient.Connected;
        }

        #region Singleton

        public static TwitchChatClient instance { get; private set; }

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

        #endregion Singleton

    }
}
