using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BirdMovement : NetworkBehaviour
{
    private CharacterController chrCtrl;
    public Camera playerCam;

    // Checks for controls
    private bool isJumpPressed = false;
    private bool isJumpHeld = false;
    private bool isForwardPressed = false;
    private bool isBackwardPressed = false;
    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    // Movement vectors
    private Vector3 forwardDirections = Vector3.zero; // Movement vector for XZ momentum
    private Vector3 moveDirections = Vector3.zero; // Final movement vector

    // Flapping in midair grants a momentum bonus
    public int maxMidairSpeedFlaps = 10;
    private int midairSpeedFlaps = 0;
    private bool initialJump = true; // Checks for ground jumps

    // Movement variables
    public float baseMoveSpeed = 4.0f; // Base movement speed
    public float backwardMovePenalty = 0.5f; // Backward slowdown factor
    public float midairMoveSpeedPortion = 0.5f; // What percentage of the base move speed will be used for momentum bonus
    public float momentumDecayMod = 0.99f; // Momentum slows down if not holding forward
    private float midairMoveSpeedBonus; // baseMoveSpeed * midairMoveSpeedPortion

    public float rotationSpeed = 0.8f;

    public float jumpSpeed = 2.0f; // Jump speed on ground
    public float midairJumpMod = 2.0f; // What factor gets applied to the base jump speed in midair

    public float gravity = 15.0f; // Base falling speed
    public float gravityMod = 0.5f; // Falling speed modifier while gliding

    // Stamina variables
    public Text staminaText;

    public int maxStamina = 10000;
    private int currentStamina;

    public int staminaRegenRate = 8;
    public int staminaFlapCost = 120;
    public int staminaGlideDecayRate = 2;

    void Start()
    {
        chrCtrl = GetComponent<CharacterController>();
        midairMoveSpeedBonus = baseMoveSpeed * midairMoveSpeedPortion; // Calculate momentum bonus
        currentStamina = maxStamina;
    }

    void Update()
    {
        if(!isLocalPlayer)
        {
            playerCam.enabled = false;
            return;
        }

        movePlayer();
    }

    void movePlayer()
    {
        // Check the player's inputs
        isJumpPressed = Input.GetButton("Jump");
        isForwardPressed = (Input.GetAxisRaw("Vertical") == 1);
        isBackwardPressed = (Input.GetAxisRaw("Vertical") == -1);
        isLeftPressed = (Input.GetAxisRaw("Horizontal") == -1);
        isRightPressed = (Input.GetAxisRaw("Horizontal") == 1);

        if(chrCtrl.isGrounded) // While player is on the ground
        {
            initialJump = true; // initial jump reset
            midairSpeedFlaps = 0; // Speed flap reset

            // Momentum reset
            moveDirections.x = 0;
            moveDirections.z = 0;
            forwardDirections = Vector3.zero;

            // Grounded jump
            if (isJumpPressed && initialJump && !isBackwardPressed)
            {
                moveDirections.y = jumpSpeed;
            }
            else
            {
                if(currentStamina < maxStamina)
                {
                    currentStamina += staminaRegenRate;
                }
                else
                {
                    currentStamina = maxStamina;
                }
            }
        }
        else
        {
            // Rotation controls
            if (isLeftPressed)
            {
                transform.Rotate(0, -rotationSpeed, 0);
            }
            else if (isRightPressed)
            {
                transform.Rotate(0, rotationSpeed, 0);
            }
            else
            {
                transform.Rotate(Vector3.zero);
            }

            // Jump / Flap controls
            if (currentStamina >= staminaFlapCost && isJumpPressed && !isJumpHeld)
            {
                initialJump = false;
                if(midairSpeedFlaps < maxMidairSpeedFlaps)
                {
                    ++midairSpeedFlaps;
                }
                moveDirections.y = jumpSpeed * midairJumpMod;
                currentStamina -= staminaFlapCost;
            }
            else if(currentStamina >= staminaGlideDecayRate && !initialJump && isJumpPressed && isJumpHeld && moveDirections.y < 0)
            {
                moveDirections.y -= gravity * gravityMod * Time.deltaTime;
                currentStamina -= staminaGlideDecayRate;
            }
            else
            {
                moveDirections.y -= gravity * Time.deltaTime;
            }

            if(currentStamina < 0)
            {
                currentStamina = 0;
            }

            // Forward Momentum
            if (isForwardPressed)
            {
                forwardDirections = transform.TransformDirection(Vector3.forward) * (baseMoveSpeed + (midairMoveSpeedBonus * midairSpeedFlaps));
            }
            else
            {
                midairSpeedFlaps = 0;
                if (isBackwardPressed && !initialJump)
                {
                    forwardDirections = transform.TransformDirection(Vector3.back) * baseMoveSpeed * backwardMovePenalty;
                }
                else
                {
                    forwardDirections *= momentumDecayMod;
                }
            }

            // Apply momentum to final movement vector
            moveDirections.x = forwardDirections.x;
            moveDirections.z = forwardDirections.z;
        }

        staminaText.text = "STAMINA: " + (currentStamina / 100) + "." + (currentStamina % 100) + "%";

        //Debug.Log("Directions: " + moveDirections); // DEBUG
        chrCtrl.Move(moveDirections * Time.deltaTime); // Apply movement
        isJumpHeld = Input.GetButton("Jump"); // Check for hold input
    }

}
