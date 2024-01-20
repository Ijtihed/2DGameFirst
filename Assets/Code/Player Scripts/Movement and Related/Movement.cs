using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    [Header("Player Settings")]
    [Tooltip("Reference to the player's Rigidbody2D component.")]
    public Rigidbody2D player;

    [Header("Speed and Movement (Main)")]
    public float playerSpeed; // Basic player speed value
    private Vector2 moveDirection;
    public bool isMoving; // Check if player is currently moving

    [Header("Sprinting Settings (Secondary)")]
    [Tooltip("Additional speed boost applied when sprinting.")]
    public float sprintBoost; // Amount the player gets as a speed boost, more than 1 required
    public bool isSprinting = false;
    public float stamina; // Current stamina amount
    public float maxStamina; 
    public Slider staminaBar; // Game Object for stamina bar
    public float staminaLoss; // How much stamina is lost per second
    public float staminaRegenThreshold; // Threshold below which stamina regen stops
    public float regenCooldown;// Cooldown for stamina in seconds
    private float regenCooldownTimer; // Timer to track cooldown
    public Image fillAreaRed;


    [Header("Controls for Movement")] // Defining controls to be edited in the UI
    public KeyCode Sprint; 
    public KeyCode moveLeft_key;
    public KeyCode moveRight_key;
    public KeyCode moveUp_key;
    public KeyCode moveDown_key;

    private void FixedUpdate()
    {
        ProcessInputs();
        Move();
        UpdateStamina();
    }
    private void Start()
    {
        player = GetComponent<Rigidbody2D>(); // Define the rigid body of the character
        StaminaSetup();
    }

    void ProcessInputs()
    {
        //Function for Processing Movement + Sprinting Inputs
        moveDirection = Vector2.zero;

        //Basic Movement (8 directional, Shortened)
        if (Input.GetKey(moveLeft_key)) { moveDirection.x = -1; }
        if (Input.GetKey(moveRight_key)) { moveDirection.x = 1; }
        if (Input.GetKey(moveUp_key)) { moveDirection.y = 1; }
        if (Input.GetKey(moveDown_key)) { moveDirection.y = -1; }

        //Sprinting Input (Shortened)
        if (Input.GetKey(Sprint) && stamina > 0 && isMoving)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        // Check if the player is moving for sprinting and stealth
        isMoving = player.velocity.magnitude > 0.01f;
    }

    void Move()
    {
        //Function for Movement + Sprinting
        if (isSprinting == true)
        {
            player.velocity = new Vector2(moveDirection.x * playerSpeed * sprintBoost, moveDirection.y * playerSpeed * sprintBoost);
        }
        else
        {
            player.velocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);
        }
    }
    private void StaminaSetup()
    {
        // Setting the stamina as the max to make them equal at first, so no errors happen
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
        maxStamina = stamina;
    }
    private void DecreaseStamina()
    {
        if (stamina > 0)
        {
            float newStamina = stamina - staminaLoss * Time.deltaTime;
            if (newStamina < staminaRegenThreshold && stamina >= staminaRegenThreshold)
            {
                // Start or reset the cooldown timer
                regenCooldownTimer = regenCooldown;
            }
            stamina = Mathf.Max(newStamina, 0); // Ensure stamina doesn't go below 0
        }
    }

    private void IncreaseStamina()
    {
        if (regenCooldownTimer <= 0) // Check if cooldown is over
        {
            stamina += staminaLoss * Time.deltaTime;
            stamina = Mathf.Min(stamina, maxStamina); // Ensure stamina doesn't exceed max
        }
    }

    private void UpdateStamina() // Check for which to do of the two above
    {
        if (isSprinting && isMoving)
        {
            DecreaseStamina();
        }
        else
        {
            IncreaseStamina();
        }

        // Decrease the cooldown timer
        if (regenCooldownTimer > 0)
        {
            regenCooldownTimer -= Time.deltaTime;
        }

        staminaBar.value = stamina; // Update the bar for stamina

        if (stamina < staminaRegenThreshold)
        {
            fillAreaRed.color = Color.red; // Stamina is below threshold, set to red
        }
        else
        {
            fillAreaRed.color = Color.green; // Stamina is above threshold, set to green
        }

    }

}