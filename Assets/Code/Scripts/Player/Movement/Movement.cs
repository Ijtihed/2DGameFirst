using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Player Settings")]
    [Tooltip("Reference to the player's Rigidbody2D and Anim component.")]
    public Rigidbody2D player;
    public Animator anim;

    [Header("Speed and Movement (Main)")]
    public float playerSpeed; // Basic player speed value
    private Vector2 moveDirection;
    public bool isMoving; // Check if player is currently moving
    private Vector2 lastMoveDirection;

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
        Animate();
    }

    void ProcessInputs()
    {
        Vector2 inputDirection = Vector2.zero;

        if (Input.GetKey(moveLeft_key)) { inputDirection.x = -1; }
        if (Input.GetKey(moveRight_key)) { inputDirection.x = 1; }
        if (Input.GetKey(moveUp_key)) { inputDirection.y = 1; }
        if (Input.GetKey(moveDown_key)) { inputDirection.y = -1; }

        if (inputDirection != Vector2.zero)
        {
            moveDirection = inputDirection;
            lastMoveDirection = inputDirection; // Update lastMoveDirection only when there's input
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    void Move()
    {
        if (isMoving)
        {
            // Function for Movement
            player.velocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);
        }
        else
        {
            // Immediately stop the player when there's no input
            player.velocity = Vector2.zero;
        }
        anim.SetBool("isMoving", isMoving);
    }

    void Animate()
    {
        if (isMoving)
        {
            // Use moveDirection for animation when the player is moving
            anim.SetFloat("AnimMoveX", moveDirection.x);
            anim.SetFloat("AnimMoveY", moveDirection.y);
        }
        else
        {
            // Use lastMoveDirection to maintain the last facing direction in idle
            anim.SetFloat("AnimMoveX", lastMoveDirection.x);
            anim.SetFloat("AnimMoveY", lastMoveDirection.y); // Keep the lastMoveDirection.y for up and down idle animations
        }
    }

}
