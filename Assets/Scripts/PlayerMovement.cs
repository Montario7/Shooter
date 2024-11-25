using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    private Vector3 playervelocity;
    public float jumpHeight = 3f;

    void Start()
    {
        // Get the CharacterController component attached to the player
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input)
    {
        // Create a movement vector based on the input
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x; // Left/Right movement
        moveDirection.z = input.y; // Forward/Backward movement

        // Transform the move direction to match the player's orientation
        Vector3 worldMoveDirection = transform.TransformDirection(moveDirection);

        // Apply the movement using the CharacterController
        controller.Move(worldMoveDirection * speed * Time.deltaTime);

        // Apply gravity
        playervelocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        controller.Move(playervelocity * Time.deltaTime);

        // Reset velocity when grounded
        if (isGrounded && playervelocity.y < 0)
            playervelocity.y = -2f;

    
    }

    public void Jump()
    {
        if (isGrounded)
        {
            // Calculate jump velocity
            playervelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
}
