using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickConsoleToggler : MonoBehaviour
{
    [SerializeField] ConsoleGUI console;
    [SerializeField] KeyCode toggleKey = KeyCode.Quote;

    public KeyCode ToggleKey => toggleKey;
    
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            console.ToggleConsole();
    }
}
