using System.Collections;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class FPSDisplay : MonoBehaviour {
    [SerializeField] private Rect startRect = new Rect(10, 10, 145, 40);
    [SerializeField] private float fps;
    [SerializeField] private int MaxFPS = 0;
    [SerializeField] private int MinFPS = 9999;
    public bool updateColor = true;
    public bool allowDrag = true;
    public float frequency = 0.5F;
    private float accum = 0f;
    private int frames = 0;
    private Color color = Color.white;
    private GUIStyle style;

#if UNITY_EDITOR
    [MenuItem("GameObject/Utils/FPSDisplay", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand) {
        GameObject go = new GameObject("FPSDisplay");
        go.AddComponent<FPSDisplay>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
#endif

    void Start() {
        StartCoroutine(FPS());
        InvokeRepeating(nameof(GetMinMax), 1f, 1f);
    }

    void Update() {
        accum += Time.timeScale / Time.deltaTime;
        ++frames;    
    }

    void GetMinMax() {
        if(MaxFPS < fps)
            MaxFPS = (int)Mathf.Max(fps);
        if(MinFPS > fps)
            MinFPS = (int)Mathf.Min(fps);
    }

    IEnumerator FPS() {
        while(true) {
            fps = accum / frames;
            color = (fps >= 30) ? new Color(0, 255, 0) : ((fps > 10) ? new Color(255, 0, 0) : new Color(255, 165, 0));
            accum = 0;
            frames = 0;
            yield return new WaitForSeconds(frequency);
        }
    }

    void OnGUI() {
        if(style == null) {
            style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
        }
        GUI.color = updateColor ? color : Color.white;
        startRect = ClampToScreen(GUI.Window(0, startRect, FPSWindow, ""));
    }

    void FPSWindow(int windowID) {
        GUI.Label(new Rect(0, 0, startRect.width, startRect.height), $"{(int)fps} FPS\n[{MinFPS}Min {MaxFPS}Max]", style);
        if(allowDrag) GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }

    private Rect ClampToScreen(Rect r) {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
}

