#region

using TwitchUnityBridge.Client;
using TwitchUnityBridge.Data;
using TwitchUnityBridge.Parser;
using UnityEngine.Assertions;

#endregion

public class MessageParseTest
{
    private const string COMMAND_PREFIX = "!";

    public void ReadMessageLoginTest()
    {
        const string PAYLOAD = ":tmi.twitch.tv 001 francoe1 :Welcome, GLHF!";
        Assert.IsTrue(new TwitchInputLine(PAYLOAD, COMMAND_PREFIX).Type == TwitchInputType.LOGIN);
    }

    public void ReadMessageNoticeTest()
    {
        const string PAYLOAD = ":tmi.twitch.tv NOTICE * :Improperly formatted auth";
        Assert.IsTrue(new TwitchInputLine(PAYLOAD, COMMAND_PREFIX).Type == TwitchInputType.NOTICE);
    }

    public void ReadMessageJoinTest()
    {
        const string PAYLOAD = ":francoe1!francoe1@francoe1.tmi.twitch.tv JOIN #rhomita";
        Assert.IsTrue(new TwitchInputLine(PAYLOAD, COMMAND_PREFIX).Type == TwitchInputType.JOIN);
    }

    public void ReadMessageCommandTest()
    {
        const string PAYLOAD =
            "@badge-info=subscriber/1;badges=moderator/1,subscriber/0;client-nonce=60576a3018294221da54321a7397db58;color=;display-name=francoe1;emotes=;first-msg=0;flags=;id=9e4e255d-3c8d-4cdb-88ca-62ab7a3c37a9;mod=1;room-id=130747120;subscriber=1;tmi-sent-ts=1635094303777;turbo=0;user-id=147383910;user-type=mod :francoe1!francoe1@francoe1.tmi.twitch.tv PRIVMSG #rhomita :!start";
        Assert.IsTrue(new TwitchInputLine(PAYLOAD, COMMAND_PREFIX).Type == TwitchInputType.MESSAGE_COMMAND);
    }

    public void ReadMessageChatTest()
    {
        const string PAYLOAD =
            "@badge-info=subscriber/1;badges=moderator/1,subscriber/0;client-nonce=b185f1767a4a2a8d5786a41f9de64a77;color=;display-name=francoe1;emotes=;first-msg=0;flags=;id=e83072fb-51b3-4217-a746-ff6e23ae09b7;mod=1;room-id=130747120;subscriber=1;tmi-sent-ts=1635094309746;turbo=0;user-id=147383910;user-type=mod :francoe1!francoe1@francoe1.tmi.twitch.tv PRIVMSG #rhomita :hola";
        Assert.IsTrue(new TwitchInputLine(PAYLOAD, COMMAND_PREFIX).Type == TwitchInputType.MESSAGE_CHAT);
    }

    public void ReadMessageChatCommandTest()
    {
        const string COMMAND = "!start";
        string PAYLOAD =
            $"@badge-info=subscriber/1;badges=moderator/1,subscriber/0;client-nonce=60576a3018294221da54321a7397db58;color=;display-name=francoe1;emotes=;first-msg=0;flags=;id=9e4e255d-3c8d-4cdb-88ca-62ab7a3c37a9;mod=1;room-id=130747120;subscriber=1;tmi-sent-ts=1635094303777;turbo=0;user-id=147383910;user-type=mod :francoe1!francoe1@francoe1.tmi.twitch.tv PRIVMSG #rhomita :{COMMAND}";
        var command = new TwitchInputLine(PAYLOAD, COMMAND_PREFIX);
        var message = new TwitchChatMessageParser(command);
        var chatCommand = new TwitchChatCommand(message.User, message.Sent, message.Bits, message.Id);
        Assert.IsTrue(chatCommand.Command == COMMAND);
    }

    public void ReadMessageChatText()
    {
        const string MESSAGE = "This is a test text.";
        string PAYLOAD =
            $"@badge-info=subscriber/1;badges=moderator/1,subscriber/0;client-nonce=b185f1767a4a2a8d5786a41f9de64a77;color=;display-name=francoe1;emotes=;first-msg=0;flags=;id=e83072fb-51b3-4217-a746-ff6e23ae09b7;mod=1;room-id=130747120;subscriber=1;tmi-sent-ts=1635094309746;turbo=0;user-id=147383910;user-type=mod :francoe1!francoe1@francoe1.tmi.twitch.tv PRIVMSG #rhomita :{MESSAGE}";
        var command = new TwitchInputLine(PAYLOAD, COMMAND_PREFIX);
        var message = new TwitchChatMessageParser(command);
        var chatCommand = new TwitchChatMessage(message.User, message.Sent, message.Bits, message.Id);
        Assert.IsTrue(chatCommand.Message == MESSAGE);
    }

    public void ReadMessageChatTextWithEspecialCharacters()
    {
        const string MESSAGE = "BEGIN <> !� {} \\ // ' \" :) :| @: #~$%&/()=^^^^^^****���� END";
        string PAYLOAD =
            $"@badge-info=subscriber/1;badges=moderator/1,subscriber/0;client-nonce=b185f1767a4a2a8d5786a41f9de64a77;color=;display-name=francoe1;emotes=;first-msg=0;flags=;id=e83072fb-51b3-4217-a746-ff6e23ae09b7;mod=1;room-id=130747120;subscriber=1;tmi-sent-ts=1635094309746;turbo=0;user-id=147383910;user-type=mod :francoe1!francoe1@francoe1.tmi.twitch.tv PRIVMSG #rhomita :{MESSAGE}";
        var command = new TwitchInputLine(PAYLOAD, COMMAND_PREFIX);
        var message = new TwitchChatMessageParser(command);
        var chatCommand = new TwitchChatMessage(message.User, message.Sent, message.Bits, message.Id);
        Assert.IsTrue(chatCommand.Message == MESSAGE);
    }

    public void ReadMessageTest()
    {
        const string MESSAGE_ID = "";
        const string MESSAGE_SENT = "!command_name";
        const int MESSAGE_BIT = 0;
        const int MESSAGE_BADGES_COUNT = 2;
        const string USER_ID = "00000000000";
        const string USER_DISPLAY_NAME = "user_display_name";

        string PAYLOAD =
            $"@badge-info=subscriber/1;badges=moderator/1,subscriber/0;client-nonce=60576a3018294221da54321a7397db58;color=;display-name={USER_DISPLAY_NAME};emotes=;first-msg=0;flags=;id=9e4e255d-3c8d-4cdb-88ca-62ab7a3c37a9;mod=1;room-id=130747120;subscriber=1;tmi-sent-ts=1635094303777;turbo=0;user-id={USER_ID};user-type=mod :francoe1!francoe1@francoe1.tmi.twitch.tv PRIVMSG #rhomita :{MESSAGE_SENT}";
        var command = new TwitchInputLine(PAYLOAD, COMMAND_PREFIX);
        var message = new TwitchChatMessageParser(command);

        Assert.AreEqual(message.Id, MESSAGE_ID, "Invalid message id");
        Assert.AreEqual(message.Sent, MESSAGE_SENT, "Invalid message sent");
        Assert.AreEqual(message.Bits, MESSAGE_BIT, "Invalid user display name");
        Assert.AreEqual(message.Badges.Count, MESSAGE_BADGES_COUNT, "Invalid user display name");
        Assert.IsNotNull(message.User, "Invalid user data");
        Assert.AreEqual(message.User.Id, USER_ID, "Invalid user id");
        Assert.AreEqual(message.User.DisplayName, USER_DISPLAY_NAME, "Invalid user display name");
    }
}
