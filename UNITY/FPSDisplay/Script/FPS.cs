/*
 * ADD UI TEXT in your Scene 
 * Place this script on the TEXT gameobject 
 * Press Play 
 */

using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour {
    public Text _fpsText;
    public float _hudRefreshRate = .1f;
    private float _timer;

    void Start() {
       _fpsText = GetComponent<Text>();
    }

    void Update() {
        if(Time.unscaledTime > _timer) {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            if(fps <= 30)
                _fpsText.color = Color.red;
            if(fps >= 31 || fps <= 59)
                _fpsText.color = new Color(255, 165, 0);
            if(fps >= 60)
                _fpsText.color = Color.green;
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}
