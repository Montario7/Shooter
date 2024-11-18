using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1600f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body left and right
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
