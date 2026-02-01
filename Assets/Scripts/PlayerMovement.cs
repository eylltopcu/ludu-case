using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform; // Add reference to camera

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private Vector2 moveInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        // Get camera's forward and right directions (flattened to ground plane)
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        
        cameraForward.y = 0;
        cameraRight.y = 0;
        
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate movement direction relative to camera
        Vector3 move = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

        // MOVE
        controller.Move(move * moveSpeed * Time.deltaTime);

        // SMOOTH ROTATION toward movement direction
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // GRAVITY
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ANIMATION
        animator.SetBool("isWalking", move != Vector3.zero);
    }
}