using DiscordPresence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// just a DEMO !

public class update : MonoBehaviour
{
    private Rect GUI2 = new Rect((float)(Screen.width - 350), 135f, 250f, 90f);
	public InputField detail;
	public InputField state;
	public Button yourButton;

	private void Start() {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
		detail.text = PresenceManager.instance.presence.details;
		state.text = PresenceManager.instance.presence.state;
	}

	void OnClick() {
		string userID = detail.text.ToString();
		string meow = state.text.ToString();
		Debug.Log(userID + meow);
		PresenceManager.UpdatePresence(detail: userID, state: meow);
    }

	private void OnGUI() {
        GUI.color = Color.white;
        Scene activeScene = SceneManager.GetActiveScene();
        GUI2 = ClampToScreen(GUI.Window(1, GUI2, new GUI.WindowFunction(SwitchWindows1), "[Chalut =^.^= DevTool]"));
    }

	private void SwitchWindows1(int id) {
		if(GUI.Button(new Rect(5f, 20f, 125f, 25f), "Update")) {
			PresenceManager.SimpleUpdate();
		}
		if(GUI.Button(new Rect(5f, 45f, 125f, 25f), "Clear")) {
			PresenceManager.ClearPresence();
		}

		GUI.DragWindow(new Rect(0f, 0f, 10000f, 20f));//don't touch this
	}
	private Rect ClampToScreen(Rect r) {
		r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
		r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
		return r;
	}
}
