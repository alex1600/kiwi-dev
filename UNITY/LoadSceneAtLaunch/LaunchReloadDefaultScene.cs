#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class LaunchReloadDefaultScene
{
    // Enter in playmode will launch the default scene
	// Exit the playmode will load the scene where you were before entering in play mode

    private static string defaultScenePath = "Assets/0_Scenes/OfflineScene.unity"; // SET YOUR SCENE PATH HERE

    static LaunchReloadDefaultScene()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if (state == PlayModeStateChange.EnteredPlayMode) // load default scene
        {
            EditorSceneManager.LoadScene(defaultScenePath);
        }
    }
}
#endif