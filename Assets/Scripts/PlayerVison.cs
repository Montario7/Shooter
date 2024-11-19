using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playervision : MonoBehaviour
{
    public Camera Cam;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        // Get mouse input
        float mouseX = input.x;
        float mouseY = input.y;

        // Adjust xRotation based on mouse Y input
        xRotation -= mouseY * ySensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Rotate the camera up/down
        Cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player left/right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
