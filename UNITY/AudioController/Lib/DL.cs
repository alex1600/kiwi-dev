using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioController)), CanEditMultipleObjects]
public class DL : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if(GUILayout.Button("Download Youtube video")) {
            serializedObject.Update();
            AudioController.DLVIDEO(serializedObject.FindProperty("SoundFromUrl").stringValue);
        }

        if(GUILayout.Button("Open root Folder")) {
            Process.Start("explorer.exe", "/select," + Directory.GetCurrentDirectory()+ "\\Assets");
        }

        if(GUILayout.Button("Move Music in Sound directory")) {
            AudioController.MoveMusic();
        }
    }
}

