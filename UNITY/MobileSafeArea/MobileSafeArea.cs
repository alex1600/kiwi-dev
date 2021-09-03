using UnityEngine;

public class MobileSafeArea : MonoBehaviour {
    RectTransform Panel;
    Rect LastSafeArea = Rect.zero;
    private void Awake(){
        Panel = GetComponent<RectTransform> ();
        Refresh();
    }
    private void FixedUpdate (){
        Refresh();
    }
    private void Refresh (){
        Rect safeArea = GetSafeArea();
        if (safeArea != LastSafeArea)
            ApplySafeArea (safeArea);
    }
    private Rect GetSafeArea (){
        return Screen.safeArea;
    }
    private void ApplySafeArea (Rect r){
        LastSafeArea = r;
        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        Vector2 anchorMin = r.position;
        Vector2 anchorMax = r.position + r.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        Panel.anchorMin = anchorMin;
        Panel.anchorMax = anchorMax;
        #if UNITY_EDITOR
        Debug.LogFormat ("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
            name, r.x, r.y, r.width, r.height, Screen.width, Screen.height);
        #endif
    }
}
