using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour{
    private GUIStyle Meow;
    private Rect rect;
    private float deltaTime = 0f;

    void Start(){
        Meow = new GUIStyle();
        Meow.fontSize = 20;
        rect = new Rect((Screen.width % 5), (Screen.height % 5f), 0f, 0f);
        
    }
    private void OnGUI() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000f;
        float fps = 1f / deltaTime;
        if(fps <= 30) Meow.normal.textColor = new Color(255, 0, 0);
        if(fps >= 31) Meow.normal.textColor = new Color(255, 165, 0);
        if(fps >= 59) Meow.normal.textColor = new Color(0, 255, 0);
        string text = string.Format("{0:0.} fps ({1:0.0})",fps, msec);
        GUI.Label(rect, text, Meow);
    }
}
