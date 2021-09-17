using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.Video;
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    #if UNITY_EDITOR
    [MenuItem("GameObject/Utils/AudioController", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand) {
        GameObject go = new GameObject("AudioController");
        go.AddComponent<AudioController>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
#endif
    private AudioSource audioSource;
    private AudioClip myClip;
    public string SoundFromUrl;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        if(audioSource.clip != null) { return; } else {
            StartCoroutine(GetAudioClip());
        }      
    }
    //https://ciihuy.com/downloads/music.mp3
    //https://www.youtube.com/watch?v=J7IMwop3RHs
    //https://www.youtube.com/watch?v=Kbj2Zss-5GY
    IEnumerator GetAudioClip() {
        if(SoundFromUrl == null) {
            SoundFromUrl = "https://ciihuy.com/downloads/music.mp3";
        }
        using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(SoundFromUrl, AudioType.MPEG);
        //using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip($@"file://{Directory.GetCurrentDirectory()}\Assets\Sounds\Song.mp3", AudioType.MPEG);
        yield return www.SendWebRequest();
        if(!www.isDone) {
            UnityEngine.Debug.Log(www.error);
        } else {
            myClip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.volume = 0.2f;
            audioSource.clip = myClip;
            audioSource.Play();
        }
    }
    public static void DLVIDEO(string url) {
        try {
            Process process = new Process();
            var final = $"/C {Directory.GetCurrentDirectory()}\\Assets\\YoutubeDL\\youtube-dl.exe --extract-audio --audio-format mp3 --prefer-ffmpeg {url} -o %(title)s.f%(format_id)s.%(ext)s -w && ffmpeg -i %(title)s.%(ext)s -acodec libmp3lame {Directory.GetCurrentDirectory()}\\Assets\\YoutubeDL\\ %(title).mp3";
            ProcessStartInfo startInfo = new ProcessStartInfo() {
                FileName = "cmd.exe",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                Arguments = final,
            };               
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            MoveMusic();
            UnityEngine.Debug.Log("Download finish in " + Directory.GetCurrentDirectory() + "\\Assets\\Sounds\\");
        }catch(Exception e) {
            UnityEngine.Debug.Log(e);
        }
    }
    public static void MoveMusic() {
        string filepath = Directory.GetCurrentDirectory();
        string path2 = Directory.GetCurrentDirectory() + "\\Assets\\Sounds\\";
        DirectoryInfo d = new DirectoryInfo(filepath);
        foreach(var file in d.GetFiles("*.mp3")) {
            try {
                if(!Directory.Exists(path2)) {
                    Directory.CreateDirectory(path2);
                }
                if(File.Exists(path2 + file.Name))
                    File.Delete(file.FullName);
                else
                    File.Move(file.FullName, path2 + file.Name);
            }
            catch(Exception ex) {
                if(ex.Message == "ERROR_ALREADY_EXISTS")
                    File.Delete(path2);
                UnityEngine.Debug.Log($"The process failed: {ex}");
            }
        }
        try {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            EditorApplication.LockReloadAssemblies();
        }
        finally {
            EditorApplication.UnlockReloadAssemblies();
        }
    }
}


/*
MPEG layer 3	                .mp3          
Ogg Vorbis	                    .ogg       
Microsoft Wave	                .wav          
Audio Interchange File Format	.aiff / .aif  
Ultimate Soundtracker module	.mod          
Impulse Tracker module	        .it           
Scream Tracker module	        .s3m          
FastTracker 2 module	        .xm           
*/

/*
GetAudioClip("https://ciihuy.com/downloads/music.mp3", AudioType.MPEG);                             to direct playing from internet
GetAudioClip($@"file://{Directory.GetCurrentDirectory()}\Assets\Sounds\Song.mp3", AudioType.MPEG);  to play since your root game folder /Sounds
https://youtu.be/5jkYtI4GtBg
*/
