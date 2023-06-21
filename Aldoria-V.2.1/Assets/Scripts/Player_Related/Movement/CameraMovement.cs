using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Sensivity Crontroller")]
    [SerializeField, Range(10f, 1500f), Tooltip("Set the sensivity, on the X axis, of the look between 10 to 1500 units")]
    private float sensX;
    [SerializeField, Range(10f, 1500f), Tooltip("Set the sensivity, on the Y axis, of the look between 10 to 1500 units")]
    private float sensY;

    [Space(10)]
    [Header("Look Component")]
    [SerializeField, Range(-50f, -90f), Tooltip("Set the negative max look between -50 to -90 units")]
    private float negatMaxLookAngle;
    [SerializeField, Range(50f, 90f), Tooltip("Set the positive max look between 50 to 90 units")]
    private float positMaxLookAngle;

    private float rotationX;
    private float rotationY;

    private float lookX;
    private float lookY;

    [SerializeField]
    private Transform direction;

    private Vector2 mouseLook;

    private void Update()
    {
        lookX *= Time.deltaTime * sensX;
        lookY *= Time.deltaTime * sensY;

        rotationX -= lookY;
        rotationY += lookX;

        rotationX = Mathf.Clamp(rotationX, negatMaxLookAngle, positMaxLookAngle);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        direction.rotation = Quaternion.Euler(0, rotationY, 0);

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        transform.parent.Rotate(Vector3.up * mouseLook.x * sensX * Time.deltaTime);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();

        lookX = mouseLook.x;
        lookY = mouseLook.y;
    }
}
