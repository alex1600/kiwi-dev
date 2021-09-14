using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class LaunchSpecificSceneShortcut
{
	private string defaultScenePath = "Assets/1_Scenes/Splash_Scene.unity" // SET YOUR SCENE PATH HERE
	
    // click command-0 to go to the prelaunch scene and then play
    [MenuItem("Edit/Play-Unplay, But From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true){
            EditorApplication.isPlaying = false;
            return;
        }
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        //Replace the string parameter with the path to your scene
        EditorSceneManager.OpenScene(defaultScenePath); 
        EditorApplication.isPlaying = true;
    }
}
