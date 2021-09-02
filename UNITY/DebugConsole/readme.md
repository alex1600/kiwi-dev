# PURPOSE:
Having a drag & drop script to add a convenient in game debug console for unity. because testing stuff is always a mess.
# HOW TO USE:
Just drag & drop "ConsoleGUI" script onto a gameobject. you can also add the "QuickConsoleToggler" script to be able to toggle the console with one key (Quote by default).
![ConsoleGUI](https://i.imgur.com/uNZqtjv.png)
A console is cool, but if u don't have any command it's not usefull. here is a sample code for how to do it :
```csharp
ConsoleCore.AddCmd(new ConsoleCMD("ping", "respond with pong.", (args) =>
{
        ConsoleCore.Print($"{args[0]}: pong");
}));
```
