using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Player Settings")]
    [Tooltip("Reference to the player's Rigidbody2D component.")]
    public Rigidbody2D player;

    [Header("Speed and Movement (Main)")]
    public float playerSpeed;
    private Vector2 moveDirection;

    [Header("Sprinting Settings (Secondary)")]
    [Tooltip("Additional speed boost applied when sprinting.")]
    public float SprintBoost;
    private bool isSprinting = false;

    [Header("Controls for Movement")]
    public KeyCode Sprint;
    public KeyCode moveLeft_key;
    public KeyCode moveRight_key;
    public KeyCode moveUp_key;
    public KeyCode moveDown_key;

    private void FixedUpdate()
    {
        ProcessInputs();
        Move();
    }
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void ProcessInputs()
    {
        //Function for Processing Movement + Sprinting Inputs

        //Basic Movement (8 directional, Shortened)
        if (Input.GetKey(moveLeft_key)) { moveDirection.x = -1; }
        if (Input.GetKey(moveRight_key)) { moveDirection.x = 1; }
        if (Input.GetKey(moveUp_key)) { moveDirection.y = 1; }
        if (Input.GetKey(moveDown_key)) { moveDirection.y = -1; }

        //Sprinting Input (Shortened)
        if (Input.GetKey(Sprint)) { isSprinting = true; }
        else { isSprinting = false; }
    }

    void Move()
    {
        //Function for Movement + Sprinting
        if (isSprinting == true)
        {
            player.velocity = new Vector2(moveDirection.x * playerSpeed * SprintBoost, moveDirection.y * playerSpeed * SprintBoost);
        }
        else
        {
            player.velocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);
        }
    }

}
