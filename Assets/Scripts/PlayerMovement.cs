using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;


    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private Vector2 moveInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // MUST be InputValue for Send Messages
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(value.Get<Vector2>());

    }

void Update()
{
    Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

    // MOVE
    controller.Move(move * moveSpeed * Time.deltaTime);

    // ROTATE TOWARDS MOVE DIRECTION
    if (move.sqrMagnitude > 0.001f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(move);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // GRAVITY
    if (controller.isGrounded && velocity.y < 0)
        velocity.y = -2f;

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    // ANIMATION
    animator.SetBool("isWalking", move.magnitude > 0.1f);
}

}
