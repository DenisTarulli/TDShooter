using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField] private float movementForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float drag;
    [SerializeField, Range(1f, 2f)] private float fallingCompensation;
    private Vector3 forceDirection;

    [Header("References")]
    [SerializeField] private Transform visualObject;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask groundMask;
    private ThirdPersonActionsAsset playerActionsAsset;
    private InputAction move;
    private Rigidbody rb;

    // Groundcheck raycast parameters
    private readonly float groundcheckRayCompensation = 0.25f;
    private readonly float groundcheckRayMaxDistance = 0.3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonActionsAsset();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        Movement();
        ApplyDrag();
        FallCompensation();
        LookAt();
    }

    private void Movement()
    {
        forceDirection += move.ReadValue<Vector2>().x * movementForce * GetCameraRight(playerCamera);
        forceDirection += move.ReadValue<Vector2>().y * movementForce * GetCameraForward(playerCamera);

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;        

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0f;

        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
    }

    private void FallCompensation()
    {
        if (rb.velocity.y < 0f)
            rb.velocity -= Physics.gravity.y * fallingCompensation * Time.fixedDeltaTime * Vector3.down;
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            return (success: true, position: hitInfo.point);
        else
            return (success: false, position: Vector3.zero);
    }

    private void LookAt()
    {
        var (success, position) = GetMousePosition();

        if (success)
        {
            var direction = position - transform.position;

            direction.y = 0f;

            visualObject.transform.forward = direction;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new(transform.position + Vector3.up * groundcheckRayCompensation, Vector3.down);
        if (Physics.Raycast(ray, out _, groundcheckRayMaxDistance))
            return true;
        else
            return false;
    }

    private void ApplyDrag()
    {
        Vector3 vel = rb.velocity;

        Vector2 inputVector = move.ReadValue<Vector2>();

        if (inputVector.x == 0 && inputVector.y == 0)
        {
            vel.x *= drag;
            vel.z *= drag;
        }

        rb.velocity = vel;
    }
}
