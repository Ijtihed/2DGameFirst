using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Reference to the player's Rigidbody2D component.")]
    public Rigidbody2D player;

    [Header("Speed and Movement (Main)")]
    public float playerSpeed; // Basic player speed value
    private Vector2 moveDirection;
    public bool isMoving; // Check if player is currently moving

    [Header("Controls for Movement")] // Defining controls to be edited in the UI
    public KeyCode moveLeft_key;
    public KeyCode moveRight_key;
    public KeyCode moveUp_key;
    public KeyCode moveDown_key;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>(); // Define the rigid body of the character
    }

    private void FixedUpdate()
    {
        ProcessInputs();
        Move();
    }

    void ProcessInputs()
    {
        // Function for Processing Movement Inputs
        moveDirection = Vector2.zero;

        // Basic Movement (8 directional, Shortened)
        if (Input.GetKey(moveLeft_key)) { moveDirection.x = -1; }
        if (Input.GetKey(moveRight_key)) { moveDirection.x = 1; }
        if (Input.GetKey(moveUp_key)) { moveDirection.y = 1; }
        if (Input.GetKey(moveDown_key)) { moveDirection.y = -1; }

        // Check if the player is moving
        isMoving = moveDirection.magnitude > 0.01f;
    }

    void Move()
    {
        // Function for Movement
        player.velocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);
    }
}
