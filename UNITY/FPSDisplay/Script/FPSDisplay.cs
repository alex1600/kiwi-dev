/*
 * Right Click > Utils > FPSDisplay in hierarchy
 * Press Play 
 */

using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class FPSDisplay : MonoBehaviour
{
    private GUIStyle guiStyle;
    private int frameCount = 0;
    private float dt = 0.0f;
    private float fps = 0.0f;
    [SerializeField] private float updateRate = 5.0f; // 4 updates per sec.

#if UNITY_EDITOR
    [MenuItem("GameObject/Utils/FPSDisplay", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("FPSDisplay");
        go.AddComponent<FPSDisplay>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
#endif
    void Start()
    {
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 24;
    }

    void OnGUI()
    {
        frameCount++;
        dt += Time.unscaledDeltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1 / updateRate;
        }
        if (fps <= 30) guiStyle.normal.textColor = new Color(255, 0, 0);
        if (fps >= 31) guiStyle.normal.textColor = new Color(255, 165, 0);
        if (fps >= 59) guiStyle.normal.textColor = new Color(0, 255, 0);
        GUI.Label(new Rect((Screen.width % 5), (Screen.height % 5f), 0f, 0f), $"FPS: {fps:0.}", guiStyle);
    }
}
