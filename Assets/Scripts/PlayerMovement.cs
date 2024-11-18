using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller; // Character Controller
    public float speed = 12f;              // Movement speed
    public float gravity = -500f;          // Extremely strong gravity for instant fall
    public float jumpHeight = 0.1f;        // Minimal jump height for almost no airtime

    private Vector3 velocity;              // Velocity vector for movement
    private bool isGrounded;               // Is the player on the ground?

    public Transform groundCheck;          // Ground detection object
    public float groundDistance = 0.4f;    // Radius for ground detection
    public LayerMask groundMask;           // Layer mask for ground

    void Update()
    {
        // Ground detection
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset vertical velocity to keep grounded
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Tiny upward jump
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
