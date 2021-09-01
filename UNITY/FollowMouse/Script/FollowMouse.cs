using UnityEngine;

public class FollowMouse : MonoBehaviour {
    public float speed = 8.0f;
    Vector2 lastMousePosition;
    private bool invoke;
    [SerializeField]private Camera camera;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate() {
        if((Vector2)Input.mousePosition != lastMousePosition) {
            lastMousePosition = (Vector2)Input.mousePosition;
            Vector2 mousePosition = Input.mousePosition;
            Vector2 mouseScreenToWorld = camera.ScreenToWorldPoint(mousePosition);
            Vector2 position = Vector2.Lerp(transform.position, mouseScreenToWorld, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
            transform.position = position;
        } else if(!invoke) {
            Invoke(nameof(WhenMouseIsntMoving), .1f);
            invoke = true;
        }
    }

    void WhenMouseIsntMoving() {
        invoke = false;
    }

    void OnGUI() {
        GUI.skin.label.fontSize = 15;
        GUILayout.Label("Screen: " + Input.mousePosition.ToString());
    }
}
