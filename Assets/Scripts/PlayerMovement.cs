using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    
    // Walking sound
    public AudioClip[] footstepSounds; // Array for multiple footstep sounds (variation)
    public float footstepInterval = 0.5f; // Time between footsteps
    private AudioSource audioSource;
    private float footstepTimer = 0f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private Vector2 moveInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound (0) since it's the player
        audioSource.volume = 0.5f; // Adjust to taste
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move input: " + moveInput);
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
        bool isMoving = move != Vector3.zero && controller.isGrounded;
        animator.SetBool("isWalking", isMoving);
        
        // FOOTSTEP SOUNDS
        if (isMoving)
        {
            footstepTimer -= Time.deltaTime;
            
            if (footstepTimer <= 0f)
            {
                PlayFootstepSound();
                footstepTimer = footstepInterval;
            }
        }
        else
        {
            // Reset timer when not moving
            footstepTimer = 0f;
        }
    }
    
    void PlayFootstepSound()
    {
        if (footstepSounds == null || footstepSounds.Length == 0) return;
        if (audioSource == null) return;
        
        // Pick a random footstep sound for variation
        AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];
        
        if (footstep != null)
        {
            audioSource.PlayOneShot(footstep);
        }
    }
}