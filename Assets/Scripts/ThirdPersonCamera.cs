using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float sensitivity = 0.2f;
    public float fixedHeight = 0f; // Set your desired Y position

    float yaw;
    float pitch;

    void LateUpdate()
    {
        // Follow player but lock Y position
        transform.position = new Vector3(target.position.x, fixedHeight, target.position.z);

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