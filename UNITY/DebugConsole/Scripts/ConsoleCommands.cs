using System.Collections.Generic;
using UnityEngine;

public class ConsoleCommands : MonoBehaviour
{
    [SerializeField] private List<string> levelNames;

    private void Awake()
    {
        ConsoleCore.AddCmd(new ConsoleCMD("ping", "respond with pong.", (args) =>
        {
            ConsoleCore.Print($"{args[0]}: pong");
        }));

        ConsoleCore.AddCmd(new ConsoleCMD("help", "ask help", (args) =>
        {
            foreach (var cmd in ConsoleCore.consoleCmd)
            {
                ConsoleCore.Print($"{cmd.Id} - {cmd.Description}");
            }
        }));
    }
}
