using UnityEngine;
using UnityEngine.InputSystem;

//The player need CharacterController to move
[RequireComponent(typeof(CharacterController))]
public class PlayerMovementBaseMotor : MonoBehaviour
{
    private CharacterController characterController;

    //Input direction
    private Vector2 movementInput;
    private Vector3 direction;

    [Header("Player Speed Control")]
    [SerializeField, Range(.2f, 10f), Tooltip("The player walk speed between .2 to 10 units")] 
    private float speed;

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

    private void Awake()
    {
        //Get the component
        characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        PerformGravity();
        PerformMove();
    }


    private void PerformMove()
    {
        characterController.Move(direction * speed * Time.deltaTime);
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
        }

        direction.y = velocity;
    }

    private bool IsGrounded() => characterController.isGrounded;
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        direction = new Vector3(movementInput.x, 0f, movementInput.y);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!IsGrounded()) return;

        velocity += jumpPower;
    }
}
