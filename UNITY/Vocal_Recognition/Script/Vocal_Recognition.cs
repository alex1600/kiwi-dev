using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class Vocal_Recognition : MonoBehaviour {

    public string[] keywords = new string[] { "up", "down", "left", "right" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public float speed = 1;

    public Text results;
    public Text mic;
    public Text Error;

    private PhraseRecognizer recognizer;
    protected string word = "right";


    // Start is called before the first frame update
    void Start() {
        foreach(var device in Microphone.devices) {
            mic.text = device;
            Debug.Log("Name: " + device);
        }
        try {
            if(keywords != null) {
                recognizer = new KeywordRecognizer(keywords, confidence);
                recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
                recognizer.Start();
                Debug.Log(recognizer.IsRunning);
            }
        }
        catch(Exception ex) {
            Error.text = ex.Message;
            Debug.LogError(ex.Message);
        }

    }
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        word = args.text;
        results.text = "You said: <b>" + word + "</b>";
    }

    // Update is called once per frame
    void Update() {
        //  var x = target.transform.position.x;
        //  var y = target.transform.position.y;
        //
        //  switch(word) {
        //      case "up":
        //      y += speed;
        //      break;
        //      case "down":
        //      y -= speed;
        //      break;
        //      case "left":
        //      x -= speed;
        //      break;
        //      case "right":
        //      x += speed;
        //      break;
        //  }
        //
        //  target.transform.position = new Vector3(x, y, 0);
    }

    private void OnApplicationQuit() {
        if(recognizer != null && recognizer.IsRunning) {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}
