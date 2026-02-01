using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionSystem : MonoBehaviour
{
    public Transform cameraPivot; // Use the PIVOT, not the camera itself
    public float interactionRange = 10f;
    public LayerMask interactableLayer;
    public GameObject Door;
    public GameObject Key;
    public GameObject LightSwitch;
    public GameObject LockedDoor;
    public GameObject Chest;
    public TMPro.TextMeshProUGUI feedbackText;
    public bool haskey = false;

    
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
                    
                    if (GameObject.ReferenceEquals(hitObject, Door))
                    {
                        feedbackText.text = "Press E to interact with Door";
                    }
                    else if (GameObject.ReferenceEquals(hitObject, Key))
                    {
                        feedbackText.text = "Press E to pick up Key";
                    }
                    else if (GameObject.ReferenceEquals(hitObject, LightSwitch))
                    {
                        feedbackText.text = "Press E to toggle Light Switch";
                    }
                    else if (GameObject.ReferenceEquals(hitObject, LockedDoor))
                    {
                        feedbackText.text = "This door is locked! Find the key.";
                        if (haskey)
                        {
                            feedbackText.text = "Press E to unlock and open the door";
                        }
                    }
                    else if (GameObject.ReferenceEquals(hitObject, Chest))
                    {
                        feedbackText.text = "Hold E to open Chest";
                    }
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
            feedbackText.text = " ";
        }
        currentLookTarget = null;
    }
}