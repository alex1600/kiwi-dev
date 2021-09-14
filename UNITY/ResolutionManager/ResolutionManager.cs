using System.Collections.Generic;
using UnityEngine;

//List containing all resolutions
[System.Serializable] 
public class ResItem {
    public int widht;
    public int height;
}

public class ResolutionManager : MonoBehaviour {
    public List<ResItem> resolutions = new List<ResItem>();
    public int currentResIndex = 0;
    private bool fullScreenMode;
    
    //Link this method to UI > Dropdown > OnValueChanged
    public void SetResolution(int resIndex){
        currentResIndex = resIndex;
        // Use resIndex to retrieve later the resolution selected by the player, example with a SaveManager:
        // SaveManager.instance.resolutionIndex = resIndex;
        ApplyResolution();
    }
    
    //Link this method to UI > Toggle > OnValueChanged
    public void SetFullScreenMode(bool fullScreen){
        fullScreenMode = fullScreen;
        // Use fullScreen to retrieve later the choice selected by the player, example with a SaveManager:
        // SaveManager.instance.fullScreenMode = fullScreen;
        ApplyResolution();
    }
    
    public void ApplyResolution(){
        Screen.SetResolution(resolutions[currentResIndex].widht, resolutions[currentResIndex].height, fullScreenMode);
    }
    
}
