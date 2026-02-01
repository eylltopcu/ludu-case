using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float sensitivity = 0.2f;

    float yaw;
    float pitch;

void LateUpdate()
    {
        // Follow player
        transform.position = target.position;

        // Left mouse button held
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * sensitivity;
            pitch -= mouseDelta.y * sensitivity;
            pitch = Mathf.Clamp(pitch, -30f, 60f);
        }

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
