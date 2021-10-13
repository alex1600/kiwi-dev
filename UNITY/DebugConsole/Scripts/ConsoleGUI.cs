using System;
using UnityEngine;

enum ConsoleAnchorPosition{
    Top,
    Bottom
}

public class ConsoleGUI : MonoBehaviour {
    [SerializeField] private GUIStyle fontConsoleStyle;
    [SerializeField] private GUIStyle fontInConsoleStyle;
    [SerializeField] private int customFontSize = 12;
    [SerializeField] private QuickConsoleToggler consoleToggler;
    
    [SerializeField] private bool isConsoleDisplayed;
    [SerializeField] private ConsoleAnchorPosition consoleAnchorPosition = ConsoleAnchorPosition.Bottom;
    [SerializeField] private float consoleHeightRatio = 0.35f;
    [SerializeField] private string consoleName = "Debug Console";

    private Vector2 _consoleScrollViewPosition;
    private int _consoleHistoryCursor;
    private const int ConsoleInputHistorySize = 8;
    private string[] _consoleInputStringHistory;
    private string _consoleInputString;
    private bool _isFocused;
    
    public void ToggleConsole(){
        _isFocused = false;
        isConsoleDisplayed = !isConsoleDisplayed;
    }

    private void Awake(){
        _consoleScrollViewPosition = Vector2.zero;
        _consoleInputString = string.Empty;
        _consoleInputStringHistory = new string[ConsoleInputHistorySize];
        fontConsoleStyle = new GUIStyle("label")
        {
            fontSize = customFontSize,
            name = "inputTf"
        };
        fontInConsoleStyle = new GUIStyle("textField")
        {
            fontSize = customFontSize
        };
    }

    private void OnGUI(){
        if (!isConsoleDisplayed)
            return;

        if (Event.current.type == EventType.KeyDown)
        {
            switch (Event.current.keyCode)
            {
                case KeyCode.Return:
                case KeyCode.KeypadEnter:
                    if (String.IsNullOrEmpty(_consoleInputString) == false)
                        ExecCmd();
                    break;
                case KeyCode.UpArrow:
                    _consoleHistoryCursor = 1 + _consoleHistoryCursor % ConsoleInputHistorySize;
                    if (_consoleInputStringHistory[ConsoleInputHistorySize - _consoleHistoryCursor] != null)
                        _consoleInputString = _consoleInputStringHistory[ConsoleInputHistorySize - _consoleHistoryCursor];
                    break;
                case KeyCode.DownArrow:
                    _consoleHistoryCursor = _consoleHistoryCursor <= 1 ? 1 : (_consoleHistoryCursor - 1);
                    if (_consoleInputStringHistory[ConsoleInputHistorySize - ConsoleInputHistorySize] != null)
                        _consoleInputString = _consoleInputStringHistory[ConsoleInputHistorySize - _consoleHistoryCursor];
                    break;
            }

            if (Event.current.keyCode == consoleToggler.ToggleKey)
            {
                ToggleConsole();
                return;
            }
        }

        Rect consoleRect = GetConsoleRect();

        GUI.Box(consoleRect, consoleName);

        GUILayout.BeginArea(consoleRect);
        
        _consoleScrollViewPosition = GUILayout.BeginScrollView(_consoleScrollViewPosition);

        GUILayout.TextField(ConsoleCore.TextBuffer, fontConsoleStyle);
        GUILayout.EndScrollView();
        GUI.SetNextControlName("inputTf");
        _consoleInputString = GUILayout.TextField(_consoleInputString, fontInConsoleStyle);
        GUILayout.EndArea();
        
        if (_isFocused) return;
        _isFocused = true;
        GUI.FocusControl("inputTf");
    }

    private void ExecCmd(){
        Array.Copy(_consoleInputStringHistory, 1, _consoleInputStringHistory, 0, ConsoleInputHistorySize - 1);
        _consoleInputStringHistory[ConsoleInputHistorySize - 1] = _consoleInputString;
        _consoleHistoryCursor = 0;

        ConsoleCore.ExecCmd(_consoleInputString);

        _consoleInputString = string.Empty;
        _consoleScrollViewPosition = new Vector2(0, float.MaxValue);
    }

    private Rect GetConsoleRect()
    {
        return consoleAnchorPosition switch
        {
            ConsoleAnchorPosition.Top => new Rect(0, 0, Screen.width, Screen.height * consoleHeightRatio),
            ConsoleAnchorPosition.Bottom => new Rect(0, Screen.height * (1 - consoleHeightRatio), Screen.width,
                Screen.height * consoleHeightRatio),
            _ => new Rect(0, 0, 0, 0)
        };
    }
}
