using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConsoleCore
{
    public static List<BaseCMD> consoleCmd = new List<BaseCMD>();
    static public string TextBuffer { get; private set; } = string.Empty;

    static public void AddCmd(BaseCMD cmd)
    {
        consoleCmd.Add(cmd);
    }
    static public void Print(string msg)
    {
        TextBuffer += $"{msg}\n";
    }
    static public void ExecCmd(string cmd)
    {
        string[] args = ParseCmd(cmd);

        int cmdIndex = GetIndexOfCmdByName(args[0]);

        if (cmdIndex == -1)
        {
            Print($"{args[0]} : Unknown Command.");
            return;
        }

        consoleCmd[cmdIndex].Invoke(args);
    }

    static private int GetIndexOfCmdByName(string name)
    {
        int i = 0;

        foreach(BaseCMD cmd in consoleCmd)
        {
            if (cmd.Id == name)
                return i;
            i++;
        }

        return -1;
    }

    static private string[] ParseCmd(string cmd)
    {
        List<string> args = new List<string>();

        int lastIndex = 0;
        bool isString = false;

        foreach (var item in cmd.Select((value, i) => new { i, value }))
        {
            if (item.value == ' ' && !isString && item.i > lastIndex)
            {
                args.Add(cmd.Substring(lastIndex, item.i - lastIndex));
                lastIndex = item.i + 1;
            }

            if (item.value == '"')
            {
                if (!isString)
                    lastIndex = item.i + 1;
                else
                {
                    args.Add(cmd.Substring(lastIndex, item.i - lastIndex));
                    lastIndex = item.i + 2;
                }

                isString = !isString;
            }
        }
        if (lastIndex < cmd.Length)
            args.Add(cmd.Substring(lastIndex, cmd.Length - lastIndex));
        
        return args.ToArray();
    }
}
