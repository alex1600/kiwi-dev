using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour {
    public List<ResItem> resolutions = new List<ResItem>();
    public int currentResIndex = 0;
    private bool fullScreenMode;
    //Link this method to UI > Dropdown > OnValueChanged
    public void SetResolution(int resIndex){
        currentResIndex = resIndex;
        SaveManager.instance.settingsData.resolutionIndex = resIndex;
        ApplyResolution();
    }
    //Link this method to UI > Toggle > OnValueChanged
    public void SetFullScreenMode(bool fullScren){
        fullScreenMode = fullScren;
        SaveManager.instance.settingsData.fullScreenMode = fullScren;
        ApplyResolution();
    }
    public void ApplyResolution(){
        Screen.SetResolution(resolutions[currentResIndex].widht, resolutions[currentResIndex].height, fullScreenMode);
    }
}
//List containing all resolutions
[System.Serializable] public class ResItem{
    public int widht, height;
}
