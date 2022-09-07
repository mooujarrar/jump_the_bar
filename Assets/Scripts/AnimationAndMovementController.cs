 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class AnimationAndMovementController : MonoBehaviour
{
    // Events to send to other components
    public static AnimationAndMovementController current;

    // Global components of the character
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // Hash int for the animator triggers
    int runningHash;
    int isGroundedHash;
    int roundWonHash;
    int isJumpingHash;
    int isFallingHash;
    int jumpCountHash;

    // gravity types to insure gravity is there
    float gravity = -9.8f;
    float groundedGravity = -1f;


    // Navigation variables
    bool isMovementPressed = false;
    Quaternion initialRotation;
    Vector3 currentMovement;

    // Factor used to rotate the face of the character to the targeted position
    float rotationFactorPerFrame = 15.0f;

    // Jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 2.0f;
    float maxJumpTime = 0.5f;
    bool isJumping = false;
    int jumpCount = 1;

    // Initial fall
    bool initialFall = true;
    bool enableMovement = false;

    // Event from the countdown timed
    bool beginGame = false;

    Coroutine currentJumpResetRoutine;

    private void Awake()
    {
        current = this;
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        initialRotation = transform.rotation;

        runningHash = Animator.StringToHash("GroundVelocity");
        isGroundedHash = Animator.StringToHash("isGrounded");
        roundWonHash = Animator.StringToHash("roundWon");
        isJumpingHash = Animator.StringToHash("isJumping");
        jumpCountHash = Animator.StringToHash("JumpCount");
        isFallingHash = Animator.StringToHash("isFalling");

        playerInput.Caractercontrol.Jump.started += onJump;
        playerInput.Caractercontrol.Jump.canceled += onJump;
        playerInput.Caractercontrol.Turn.started += onMouvementInput;
        playerInput.Caractercontrol.Turn.performed += onMouvementInput;
        playerInput.Caractercontrol.Turn.canceled += onMouvementInput;

        setupJumpVariables();
        animator.SetBool(isFallingHash, initialFall);
    }

    public event Action onGroundRotationStart;

    void onJump(InputAction.CallbackContext context)
    {
        if (enableMovement == true && beginGame == true)
        {
            isJumpPressed = context.ReadValueAsButton();
        }
    }

    void onMouvementInput(InputAction.CallbackContext context)
    {
        if(enableMovement == true && beginGame == true)
        {
            currentMovement.z = context.ReadValue<float>() * 1.0f; //0.8
            currentMovement.x = context.ReadValue<float>() * -0.0f; //-0.2
            isMovementPressed = currentMovement.z != 0 || currentMovement.x != 0;
        }
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2.0f;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            if(jumpCount < 2 && currentJumpResetRoutine != null)
            {
                StopCoroutine(currentJumpResetRoutine);
            }
            isJumping = true;
            jumpCount += 1;
            currentMovement.y = initialJumpVelocity * .5f;
        } else if (isJumping && characterController.isGrounded && !isJumpPressed)
        {
            isJumping = false;
            currentJumpResetRoutine = StartCoroutine(jumpResetRoutine());
        }
        animator.SetBool(isJumpingHash, isJumping);
        if (jumpCount > 2)
        {
            jumpCount = 1;
        }
        animator.SetInteger(jumpCountHash, jumpCount);
    }

    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        jumpCount = 1;
    }

    void handleAnimation()
    {
        if(characterController.isGrounded)
        {
            float actualVelocity = GroundAnimate.current.getActualVelocity();
            /*float initialVelocity = GroundAnimate.current.initialSecondsPerRound;
            float targetedVelocity = GroundAnimate.current.targetedSecondsPerRound;*/
            animator.SetFloat(runningHash, actualVelocity);
        }
    }

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f;
        float fallMultiplier = 2.0f;

        if(characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            if (animator.GetBool(isGroundedHash) == false)
            {
                animator.SetBool(isGroundedHash, true);
            }
            if(animator.GetBool(isFallingHash) == true)
            {
                initialFall = false;
                animator.SetBool(isFallingHash, false);
            }
        }
        else {
            float prevVelocity = currentMovement.y;
            float newVelocity = currentMovement.y + (gravity * Time.deltaTime * (isFalling == true ? fallMultiplier : 1.0f) * (initialFall == true ? 0.05f : 1.0f));
            currentMovement.y = (prevVelocity + newVelocity) * .5f;
            if(animator.GetBool(isGroundedHash) == true)
            {
                animator.SetBool(isGroundedHash, false);
            }
        }
    }

    public void FallFinished()
    {
        onGroundRotationStart();
        enableMovement = true;
    }

    public void BeginGame()
    {
        beginGame = true;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        } else
        {
            transform.rotation = Quaternion.Slerp(currentRotation, initialRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        characterController.Move(currentMovement * Time.deltaTime * 5.0f);
        handleGravity();
        handleJump();
    }

    private void OnEnable()
    {
        playerInput.Caractercontrol.Enable();
    }

    private void OnDisable()
    {
        playerInput.Caractercontrol.Disable();
    }
}
