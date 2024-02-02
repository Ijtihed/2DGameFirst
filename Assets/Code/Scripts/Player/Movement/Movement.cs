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
        // Check horizontal movement
        if (Input.GetKey(moveLeft_key))
        {
            moveDirection.x = -1;
        }
        else if (Input.GetKey(moveRight_key))
        {
            moveDirection.x = 1;
        }
        else
        {
            // If neither left nor right is pressed, reset the horizontal movement to 0
            moveDirection.x = 0;
        }

        // Check vertical movement
        if (Input.GetKey(moveUp_key))
        {
            moveDirection.y = 1;
        }
        else if (Input.GetKey(moveDown_key))
        {
            moveDirection.y = -1;
        }
        else
        {
            // If neither up nor down is pressed, reset the vertical movement to 0
            moveDirection.y = 0;
        }

        // Check if the player is moving
        isMoving = moveDirection.magnitude > 0.01f;
    }


    void Move()
    {
        // Function for Movement
        player.velocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed).normalized;
    }

    void Animate()
    {
        anim.SetFloat("AnimMoveX", moveDirection.x);
        anim.SetFloat("AnimMoveY", moveDirection.y);
    }
}
