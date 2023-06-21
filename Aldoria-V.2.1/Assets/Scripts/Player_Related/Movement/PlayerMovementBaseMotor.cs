using UnityEngine;
using UnityEngine.InputSystem;

//The player need CharacterController to move
[RequireComponent(typeof(CharacterController))]
public class PlayerMovementBaseMotor : MonoBehaviour
{
    private CharacterController characterController;

    //Input direction
    private Vector2 movementInput;
    private Vector3 moveDirection;

    [SerializeField]
    private Transform camHolder;

    [Header("Player Speed Control")]
    [SerializeField, Range(.2f, 10f), Tooltip("The player walk speed between .2 to 10 units")]
    private float walkSpeed;    
    [SerializeField, Range( 5f, 20f), Tooltip("The player running speed between 5 to 20 units")]
    private float runSpeed;
    //The player speed
    private float desiredMoveSpeed;

    [Space(10)]
    [Header("Gravity Control")]
    [SerializeField, Range(.2f, 3f), Tooltip("Set the amount of gravity mulptiplier between .2 to 3 units")]
    private float gravityMultiplier;

    private float gravity = -9.81f;
    private float velocity;

    [Space(10)]
    [Header("Jump Control")]
    [SerializeField, Range(1f, 8f), Tooltip("The jump power of the player between 1 and 8 units")]
    private float jumpPower;

    [Header("Debug bools")]
    [SerializeField] bool isRunning;

    private void Awake()
    {
        //Get the component
        characterController = GetComponent<CharacterController>();

        //Set the speed
        desiredMoveSpeed = walkSpeed;
    }
    private void Update()
    {
        PerformGravity();
        PerformMove();
    }


    private void PerformMove()
    {
        Vector3 goTo = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        goTo.y = moveDirection.y;

        characterController.Move(goTo * desiredMoveSpeed * Time.deltaTime);
        

    }

    private void PerformGravity()
    {
        if (IsGrounded() && velocity < 0.0f)
        {
            //Block the gravity
            velocity = -1f;
        }
        else
        {
            //Set gravity
            velocity += gravity * gravityMultiplier * Time.deltaTime;
            //Modify the player speed
            desiredMoveSpeed = walkSpeed;
            //modify bools
            isRunning = false;
        }

        moveDirection.y = velocity;
    }

    private bool IsGrounded() => characterController.isGrounded;
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && !isRunning)
        {
            isRunning = true;
            desiredMoveSpeed = runSpeed;
        }
        else if (context.performed && isRunning)
        {
            isRunning = false;
            desiredMoveSpeed = walkSpeed;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        if (!IsGrounded()) return;

        velocity += jumpPower;
    }
}
