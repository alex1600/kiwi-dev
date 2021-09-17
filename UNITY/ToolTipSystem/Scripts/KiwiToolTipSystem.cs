using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class KiwiToolTipSystem : MonoBehaviour
{
    private static KiwiToolTipSystem current;
    [SerializeField] private KiwiToolTip kiwiToolTip;

    public float distanceToPointer;

    public int characterWrapLimit;

    public enum Orientation
    {
        Top, Down, Left, Right
    }

    public Orientation orientation;

    public void Awake()
    {
        current = this;
        distanceToPointer = 0.3f;
    }

    public static void Show(string content, string header = "")
    {
        current.kiwiToolTip.SetText(content, header);
        current.kiwiToolTip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.kiwiToolTip.gameObject.SetActive(false);
    }
}


[CustomEditor(typeof(KiwiToolTipSystem))]
public class EditorKiwiToolTipSystem : Editor
{
    public override void OnInspectorGUI()
    {
        KiwiToolTipSystem ktts = (KiwiToolTipSystem)target;

        EditorGUILayout.HelpBox("Always keep this GameObject as last element of canvas.", MessageType.Error, true);

        EditorGUILayout.Separator();
        GUILayout.Label("Kiwi ToolTip", GUI.skin.button);
        EditorGUILayout.Separator();

        base.OnInspectorGUI();

    }
}