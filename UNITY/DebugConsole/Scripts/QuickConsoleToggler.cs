using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickConsoleToggler : MonoBehaviour
{
    [SerializeField]
    ConsoleGUI console;
    [SerializeField]
    KeyCode toggleKey = KeyCode.Quote;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            console.ToggleConsole();
    }
}
