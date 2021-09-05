using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFollowPlayer : MonoBehaviour
{
    public Text nameText;
    public Transform mTransform;
    public Transform mTextOverTransform;
    public float lastXVal;
    public SpriteRenderer flip;

    private Camera mainCam;
    
    // Start is called before the first frame update
    void Start()
    {
        flip = gameObject.GetComponent<SpriteRenderer>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.hasChanged) {
            if(transform.position.x < lastXVal) {
                lastXVal = transform.position.x;
                flip.flipX = true;
            } else if(transform.position.x > lastXVal) {
                lastXVal = transform.position.x;
                flip.flipX = false;
            }
            transform.hasChanged = false;
        }
    }

    void Awake() {
        mTransform = transform;
        mTextOverTransform = nameText.transform;
    }
    
    void LateUpdate() {
        Vector3 screenPos =  mainCam.WorldToScreenPoint(mTransform.position);
        screenPos.y += 1;
    }

}
