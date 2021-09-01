/*
 * Right Click > PsykoDev > FPSDisplay in hierarchy
 * Press Play 
 */

using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class FPS : MonoBehaviour {
    private GUIStyle Meow;

#if UNITY_EDITOR
    [MenuItem("GameObject/PsykoDev/FPSDisplay", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand) {
        GameObject go = new GameObject("FPSDisplay");
        go.AddComponent<FPS>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
#endif
    void Start () {
        Meow = new GUIStyle();
        Meow.fontSize = 30;
    }
     void OnGUI() {
        int fps = (int)(1f / Time.unscaledDeltaTime);
        if(fps <= 30)Meow.normal.textColor = new Color(255, 0, 0);
        if(fps >= 31)Meow.normal.textColor = new Color(255, 165, 0);  
        if(fps >= 59)Meow.normal.textColor = new Color(0, 255, 0);      
        GUI.Label(new Rect((Screen.width % 5),(Screen.height % 5f), 0f, 0f), "FPS: " + fps, Meow);
     }
}
