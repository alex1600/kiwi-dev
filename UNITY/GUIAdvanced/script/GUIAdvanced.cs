using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GUIAdvanced : MonoBehaviour
{
	private Rect GUI1 = new Rect((float)(Screen.width - 350), 5f, 350f, 100f);
	private Rect GUI2 = new Rect((float)(Screen.width - 350), 135f, 250f, 90f);


	private void OnGUI(){
		GUI.color = Color.white;
		Scene activeScene = SceneManager.GetActiveScene();
		GUI1 = GUI.Window(0, GUI1, new GUI.WindowFunction(SwitchWindow0), activeScene.name + " [Chalut =^.^=]");

		GUI2 = GUI.Window(1, GUI2, new GUI.WindowFunction(SwitchWindows1), "[Chalut =^.^= DevTool]");
	}

	private void SwitchWindow0(int id){
		GUI.Label(new Rect(1f, 20f, 175f, 25f), "myLabel1");

		if (GUI.Button(new Rect(175f, 20f, 175f, 25f), "MyButton2"))
		{

		}
		if (GUI.Button(new Rect(1f, 40f, 175f, 25f), "MyButton3"))
		{

		}
		if (GUI.Button(new Rect(175f, 40f, 175f, 25f), "MyButton4"))
		{

		}
		if (GUI.Button(new Rect(175f, 60f, 87.5f, 25f), "MyButton5"))
		{

		}
		if (GUI.Button(new Rect(262.5f, 60f, 87.5f, 25f), "MyButton6"))
		{

		}
		
		GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f)); //don't touch this
	}

	private void SwitchWindows1(int id){
		GUI.Label(new Rect(5f, 20f, 125f, 25f), "myLabel1");

		if (GUI.Button(new Rect(135f, 20f, 75f, 25f), "MyButton1"))
		{

		}
		if (GUI.Button(new Rect(5f, 45f, 125f, 25f), "MyButton2"))
		{

		}
		if (GUI.Button(new Rect(135f, 45f, 75f, 25f), "MyButton3"))
		{

		}
		GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));//don't touch this
	}
}
