using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Sprinting : MonoBehaviour
{
    private Movement movement; // Reference to the Movement script

    [Header("Sprinting Settings (Secondary)")]
    [Tooltip("Additional speed boost applied when sprinting.")]
    public float sprintBoost; // Amount the player gets as a speed boost, more than 1 required
    public bool isSprinting = false;
    public float stamina; // Current stamina amount
    public float maxStamina;
    public Slider staminaBar; // Game Object for stamina bar
    public float staminaLoss; // How much stamina is lost per second
    public float staminaRegenThreshold; // Threshold below which stamina regen stops
    public float regenCooldown; // Cooldown for stamina in seconds
    private float regenCooldownTimer; // Timer to track cooldown
    public Image fillAreaRed; // UI element to indicate low stamina

    [Header("Controls for Sprinting")] // Defining controls to be edited in the UI
    public KeyCode Sprint;

    private void Start()
    {
        movement = GetComponent<Movement>(); // Get the Movement component
        StaminaSetup();
    }

    private void FixedUpdate()
    {
        ProcessSprintInput();
        UpdateStamina();
    }

    void ProcessSprintInput()
    {
        // Sprinting Input (Shortened)
        if (Input.GetKey(Sprint) && (isSprinting || stamina > staminaRegenThreshold) && movement.isMoving)
        {
            isSprinting = true;
            movement.player.velocity *= sprintBoost;
        }
        else if (!Input.GetKey(Sprint) || stamina <= 0)
        {
            isSprinting = false;
        }
    }

    private void StaminaSetup()
    {
        // Setting the stamina as the max to make them equal at first, so no errors happen
        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
    }

    private void DecreaseStamina()
    {
        if (stamina > 0 && isSprinting)
        {
            stamina -= staminaLoss * Time.deltaTime;
            stamina = Mathf.Max(stamina, 0); // Ensure stamina doesn't go below 0
            if (stamina <= 0)
            {
                isSprinting = false; // Stop sprinting if stamina reaches 0
            }
        }

        if (stamina < staminaRegenThreshold && stamina > 0)
        {
            regenCooldownTimer = regenCooldown; // Start or reset the cooldown timer
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

    private void UpdateStamina()
    {
        if (isSprinting && movement.isMoving)
        {
            DecreaseStamina();
        }
        else
        {
            IncreaseStamina();
        }

        if (regenCooldownTimer > 0)
        {
            regenCooldownTimer -= Time.deltaTime; // Decrease the cooldown timer
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
