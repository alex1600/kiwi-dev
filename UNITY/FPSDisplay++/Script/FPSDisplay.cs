#region

using System;
using System.Collections;
using System.Diagnostics;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

#endregion

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private Rect startRect = new Rect(10, 10, 145, 50);
    [SerializeField] private float fps;
    [SerializeField] private int maxFPS;
    [SerializeField] private int minFPS = 9999;
    [SerializeField] private bool updateColor = true;
    [SerializeField] private bool allowDrag = true;
    [SerializeField] private float frequency = 0.5F;
    [SerializeField] private bool moreOptionInUI = true;
    [SerializeField] private bool moreOptionInEditor = true;
    [SerializeField] private string memsize;
    [SerializeField] private string GCmemsize;
    [SerializeField] private string GCResmemsize;
    [SerializeField] private string Sysmemsize;
    [SerializeField] private string cpuusage;
    private float _accum;
    private Color _color = Color.white;
    private int _frames;
    private ProfilerRecorder _GCReservedMemory;
    private ProfilerRecorder _GCUsedMemory;
    private GUIStyle _style;
    private ProfilerRecorder _SystemUsedMemory;
    private ProfilerRecorder _totalUsedMemory;
    private void Start()
    {
        StartCoroutine(FPS());
        InvokeRepeating(nameof(GetMinMax), 1f, 1f);
        if (moreOptionInUI)
        {
            startRect = new(10, 10, 145, 130);
            StartCoroutine(Stats());
        }
    }

    private void Update()
    {
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;
    }

    private void OnEnable()
    {
        _totalUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
        _GCUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Used Memory");
        _GCReservedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        _SystemUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
#if UNITY_EDITOR
        if (moreOptionInEditor)
        {
            _texturcount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Count");
            _texturememory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
            _materialcount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Material Count");
            _materialmemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Material Memory");
            _assetcount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Asset Count");
            _GOinscene = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GameObjects in Scenes");
            _totalGOinscene = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Objects in Scenes");
            _totalUnityOjectcount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Unity Object Count");
            _gcalloframecnt = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Allocation In Frame Count");
            _gcallofrrame = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Allocated In Frame");
        }
#endif
    }
    private void OnDisable()
    {
        _totalUsedMemory.Dispose();
        _GCUsedMemory.Dispose();
        _GCReservedMemory.Dispose();
        _SystemUsedMemory.Dispose();

#if UNITY_EDITOR
        _texturcount.Dispose();
        _texturememory.Dispose();
        _materialcount.Dispose();
        _materialmemory.Dispose();
        _assetcount.Dispose();
        _GOinscene.Dispose();
        _totalGOinscene.Dispose();
        _totalUnityOjectcount.Dispose();
        _gcalloframecnt.Dispose();
        _gcallofrrame.Dispose();
#endif
    }

    private void OnGUI()
    {
        if (_style == null)
        {
            _style = new(GUI.skin.label);
            _style.normal.textColor = Color.white;
            _style.alignment = TextAnchor.MiddleCenter;
        }
        GUI.color = updateColor ? _color : Color.white;
        if (!moreOptionInUI) startRect = ClampToScreen(GUI.Window(0, startRect, FPSWindow, ""));
        else startRect = ClampToScreen(GUI.Window(0, startRect, FPSWindow, ""));
        }

#if UNITY_EDITOR
    [MenuItem("GameObject/Utils/FPSDisplay", false, 10)]
    private static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        var go = new GameObject("FPSDisplay");
        go.AddComponent<FPSDisplay>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
#endif

    private void GetMinMax()
    {
        if (maxFPS < fps)
            maxFPS = (int)Mathf.Max(fps);
        if (minFPS > fps)
            minFPS = (int)Mathf.Min(fps);
    }

    private IEnumerator FPS()
    {
        while (true)
        {
            fps = _accum / _frames;
            _color = fps >= 30 ? new(0, 255, 0) : fps > 10 ? new(255, 0, 0) : new Color(255, 165, 0);
            _accum = 0;
            _frames = 0;
            yield return new WaitForSeconds(frequency);
        }
    }

    private IEnumerator Stats()
    {
        while (true)
        {
            var usage = new UsageRamProc();
            cpuusage = usage.GetCpuPercentage();
            memsize = FormatSize(_totalUsedMemory.LastValue);
            GCmemsize = FormatSize(_GCUsedMemory.LastValue);
            GCResmemsize = FormatSize(_GCReservedMemory.LastValue);
            Sysmemsize = FormatSize(_SystemUsedMemory.LastValue);

#if UNITY_EDITOR
            if (moreOptionInEditor)
            {
                textur_count = _texturcount.LastValue.ToString();
                texture_memory = FormatSize(_texturememory.LastValue);
                material_count = _materialcount.LastValue.ToString();
                material_memory = FormatSize(_materialmemory.LastValue);
                asset_count = _assetcount.LastValue.ToString();
                GameObject_In_Scene = _GOinscene.LastValue.ToString();
                total_GameObject_in_scene = _totalGOinscene.LastValue.ToString();
                total_Unity_Oject_count = _totalUnityOjectcount.LastValue.ToString();
                gc_alloc_framecount = _gcalloframecnt.LastValue.ToString();
                gc_alloc_in_frame = FormatSize(_gcallofrrame.LastValue);
            }
#endif

            yield return new WaitForSeconds(frequency);
        }
    }

    private string FormatSize(double value, int decimalPlaces = 1)
    {
        string[] SizeSuffixes = { "b", "KB", "MB", "GB" };
        if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
        if (value < 0) { return "-" + FormatSize(-value, decimalPlaces); }
        if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }
        int mag = (int)Math.Log(value, 1024);
        decimal adjustedSize = (decimal)value / (1L << mag * 10);
        if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
        {
            mag += 1;
            adjustedSize /= 1024;
        }
        return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
    }

    private void FPSWindow(int windowID)
    {
        if (allowDrag) GUI.DragWindow(new(0, 0, Screen.width, Screen.height));
        
        if (!moreOptionInUI) GUI.Label(new(0, 0, startRect.width, startRect.height), $"FPS Display\n{Mathf.Round(fps)} FPS\n[{minFPS}Min {maxFPS}Max]", _style);
        else GUI.Label(new(0, 0, startRect.width, startRect.height), $"FPS Display\n{Mathf.Round(fps)} FPS\n[{minFPS}Min {maxFPS}Max]\nCpu {cpuusage}% \nMem {memsize}\nGC Used {GCmemsize}\nGCRes {GCResmemsize}\nSysMem {Sysmemsize}", _style);

    }

    private Rect ClampToScreen(Rect r) {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }

#if UNITY_EDITOR
    private ProfilerRecorder _texturcount;
    private ProfilerRecorder _texturememory;
    private ProfilerRecorder _materialcount;
    private ProfilerRecorder _materialmemory;
    private ProfilerRecorder _assetcount;
    private ProfilerRecorder _GOinscene;
    private ProfilerRecorder _totalGOinscene;
    private ProfilerRecorder _totalUnityOjectcount;
    private ProfilerRecorder _gcalloframecnt;
    private ProfilerRecorder _gcallofrrame;

    [SerializeField] private string textur_count;
    [SerializeField] private string texture_memory;
    [SerializeField] private string material_count;
    [SerializeField] private string material_memory;
    [SerializeField] private string asset_count;
    [SerializeField] private string GameObject_In_Scene;
    [SerializeField] private string total_GameObject_in_scene;
    [SerializeField] private string total_Unity_Oject_count;
    [SerializeField] private string gc_alloc_framecount;
    [SerializeField] private string gc_alloc_in_frame;
#endif
}

internal class UsageRamProc
{
    private static DateTime lastTime;
    private static TimeSpan lastTotalProcessorTime;
    private static DateTime curTime;
    private static TimeSpan curTotalProcessorTime;
    private readonly Process pp = Process.GetCurrentProcess();

    public string GetCpuPercentage()
    {
        if (lastTime == null || lastTime == new DateTime())
        {
            lastTime = DateTime.Now;
            lastTotalProcessorTime = pp.TotalProcessorTime;
            return "";
        }
        curTime = DateTime.Now;
        curTotalProcessorTime = pp.TotalProcessorTime;

        double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);

        lastTime = curTime;
        lastTotalProcessorTime = curTotalProcessorTime;
        return $"{CPUUsage * 100:0.0}";
    }
}