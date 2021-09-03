using System;
using System.Collections;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using UnityEngine;
using UnityEngine.UIElements;
using System.Reflection;

enum ConsoleAnchorPosition
{
    Top,
    Bottom
}

public class ConsoleGUI : MonoBehaviour
{
    [SerializeField]
    bool isConsoleDisplayed = false;
    [SerializeField]
    ConsoleAnchorPosition consoleAnchorPosition = ConsoleAnchorPosition.Bottom;
    [SerializeField]
    float consoleHeightRatio = 0.35f;
    [SerializeField]
    string consoleName = "Debug Console";

    Vector2 consoleScrollViewPosition;
    int consoleHistoryCursor = 0;
    int consoleInputHistorySize = 8;
    string[] consoleInputStringHistory;
    string consoleInputString;

    public void ToggleConsole()
    {
        isConsoleDisplayed = !isConsoleDisplayed;
    }

    private void Awake()
    {
        consoleScrollViewPosition = new Vector2(0, 0);
        consoleInputString = string.Empty;
        consoleInputStringHistory = new string[consoleInputHistorySize];
    }

    private void OnGUI()
    {
        if (!isConsoleDisplayed)
            return;

        if (Event.current.Equals(Event.KeyboardEvent("return")) && consoleInputString != string.Empty)
            ExecCmd();

        if (Event.current.Equals(Event.KeyboardEvent("up")))
        {
            consoleHistoryCursor = 1 + consoleHistoryCursor % consoleInputHistorySize;
            if (consoleInputStringHistory[consoleInputHistorySize - consoleHistoryCursor] != null)
                consoleInputString = consoleInputStringHistory[consoleInputHistorySize - consoleHistoryCursor];
        }
        if (Event.current.Equals(Event.KeyboardEvent("Down")))
        {
            consoleHistoryCursor = consoleHistoryCursor <= 1 ? 1 : (consoleHistoryCursor - 1);
            if (consoleInputStringHistory[consoleInputHistorySize - consoleHistoryCursor] != null)
                consoleInputString = consoleInputStringHistory[consoleInputHistorySize - consoleHistoryCursor];
        }

        Rect consoleRect = GetConsoleRect();

        GUI.Box(consoleRect, consoleName);

        GUILayout.BeginArea(consoleRect);
        
        consoleScrollViewPosition = GUILayout.BeginScrollView(consoleScrollViewPosition);

        GUILayout.TextField(ConsoleCore.TextBuffer, "Label");
        GUILayout.EndScrollView();
        consoleInputString = GUILayout.TextField(consoleInputString);
        GUILayout.EndArea();
    }

    private void ExecCmd()
    {
        Array.Copy(consoleInputStringHistory, 1, consoleInputStringHistory, 0, consoleInputHistorySize - 1);
        consoleInputStringHistory[consoleInputHistorySize - 1] = consoleInputString;
        consoleHistoryCursor = 0;

        ConsoleCore.ExecCmd(consoleInputString);

        consoleInputString = string.Empty;
        consoleScrollViewPosition = new Vector2(0, float.MaxValue);
    }

    private Rect GetConsoleRect()
    {
        switch (consoleAnchorPosition)
        {
            case ConsoleAnchorPosition.Top:
                return new Rect(0, 0, Screen.width, Screen.height * consoleHeightRatio);
            case ConsoleAnchorPosition.Bottom:
                return new Rect(0, Screen.height * (1 - consoleHeightRatio), Screen.width, Screen.height * consoleHeightRatio);
            default:
                return new Rect(0, 0, 0, 0);
        }
    }
}
