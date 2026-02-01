using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
    public Transform cameraPivot; // Use the PIVOT, not the camera itself
    public float interactionRange = 10f;
    public LayerMask interactableLayer;
    
    private GameObject currentLookTarget;
    private Outline currentOutline;

    void Update()
    {
        // Shoot ray from camera pivot's position in its forward direction
        Ray ray = new Ray(cameraPivot.position, cameraPivot.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.gameObject == this.gameObject) return; // Skip player
            
            Debug.Log("HIT: " + hit.collider.gameObject.name);
            
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != currentLookTarget)
            {
                ClearOutline();
                
                currentLookTarget = hitObject;
                currentOutline = hitObject.GetComponent<Outline>();
                
                if (currentOutline != null)
                {
                    currentOutline.enabled = true;
                    Debug.Log("Outline enabled!");
                }
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                IInteractable interactable = hitObject.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact();
            }
        }
        else
        {
            ClearOutline();
        }
    }

    void ClearOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }
        currentLookTarget = null;
    }
}