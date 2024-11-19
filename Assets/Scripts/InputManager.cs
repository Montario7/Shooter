using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.InGameActions inGame; // Use InGameActions to match your action map name
    private PlayerMovement movement;
    private Playervision vision;

    void Awake()
    {
        // Initialize the PlayerInput and set the InGame action map
        playerInput = new PlayerInput();
        inGame = playerInput.InGame;

        movement = GetComponent<PlayerMovement>();
        vision = GetComponent<Playervision>();

        // Bind Jump input to the Jump method in PlayerMovement
        inGame.Jump.performed += ctx => movement.Jump();
    }

    private void LateUpdate()
    {
        // Pass the look vector to the Playervision script
        vision.ProcessLook(inGame.look.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        // Pass the movement vector to the PlayerMovement script
        Vector2 movementInput = inGame.Movement.ReadValue<Vector2>();
        movement.ProcessMove(movementInput);
    }

    private void OnEnable()
    {
        inGame.Enable(); // Enable the InGame action map
    }

    private void OnDisable()
    {
        inGame.Disable(); // Disable the InGame action map
    }
}
