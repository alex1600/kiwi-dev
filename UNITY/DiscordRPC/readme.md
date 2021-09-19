DISCORD RPC DEMO Unity Package

# HOW TO USE:
Note: Make sure your Activity status is enabled in discord settings
Extract " DiscordRPC.unitypackage " in our project, you can find a test map in Demo / Scene / Meow,

you can find in Demo / Script a lil script to help you to understand how to update our Discord status

```cs
PresenceManager.UpdatePresence(detail: "LVL 1 Mini Enfer", state: "uai uai je test"); //to update
PresenceManager.SimpleUpdate(); // Simple refresh
PresenceManager.ClearPresence(); // Clear ALL 
```
as you can see on this screenshot what's is detail and state 

![](https://cdn.discordapp.com/attachments/877432819746488380/889125206109614130/unknown.png)

in the Prefab " Presence Manager " or Script " PresenceManager " you need update your 1st Presence

![Presence Settings](https://cdn.discordapp.com/attachments/877432819746488380/889123144558850108/unknown.png)

# Make your Discord BotID

First you need to go here https://discord.com/developers/applications connect you with your account and create new app

![](https://cdn.discordapp.com/attachments/877432819746488380/889124001983643698/unknown.png)

when it's done name your app as your game name 

![](https://cdn.discordapp.com/attachments/877432819746488380/889124477881970718/unknown.png)

now you wan copy you application ID and paste it in " Presence Manage / Application ID "


# Make your Discord Image

Now your Art Assets for your game you need to go in " Rich Presence / Art Assets " and upload your game icon 

Note: .png, .jpg, or .jpeg — 1024x1024 recommended, 512x512 minimum

![](https://cdn.discordapp.com/attachments/877432819746488380/889126395907813376/unknown.png)

when it's done you need wait a bit and let Discord approve your image uploaded

now you can go in " Rich presence / Visualizer " and select your icon location 

![](https://cdn.discordapp.com/attachments/877432819746488380/889127293639856128/unknown.png)

IMPORTANT NOTE! : your icon is used as key, so keep the name and put in in your " Presence Manager " 

![](https://cdn.discordapp.com/attachments/877432819746488380/889128459908042802/unknown.png)

aight your ready now! GG


# WARNING
DON'T touch Editor folder and Plugins Folder ! otherwise Discord RPC is broke