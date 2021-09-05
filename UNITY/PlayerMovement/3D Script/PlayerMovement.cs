using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;
    private Vector3 velocityMove = Vector3.zero;

    public float speed = 0.2f;
    float x;
    float z;

    [SerializeField] private float lookSensitivity = 1f;
    [SerializeField] private float xRotation = 0f;
    [SerializeField] private GameObject cam;
    [SerializeField] private Transform playerBody;

    void Update() {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        float X = Input.GetAxis("Mouse X") * lookSensitivity;
        playerBody.Rotate(Vector3.up * X);
        float Y = Input.GetAxis("Mouse Y") * lookSensitivity;
        xRotation -= Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }   
     private void FixedUpdate() {
        velocityMove = transform.right * x + transform.forward * z;
        controller.Move(velocityMove * speed);
    }
}
