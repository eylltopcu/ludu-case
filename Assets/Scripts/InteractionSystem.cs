using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    public Transform cameraPivot;
    public float interactionRange = 10f;
    public LayerMask interactableLayer;
    public GameObject Door;
    public GameObject Key;
    public GameObject LightSwitch;
    public GameObject LockedDoor;
    public GameObject Chest;
    public TMPro.TextMeshProUGUI feedbackText;
    
    // Hold interaction UI
    public Image holdProgressBar; // Assign a UI Image with Image Type = Filled
    public GameObject holdProgressUI; // Parent GameObject containing the progress bar
    
    private GameObject currentLookTarget;
    private Outline currentOutline;
    private bool isHolding = false;
    private float holdTimer = 0f;
    private float requiredHoldTime = 2f;
    private Chest currentChest;

    void Start()
    {
        if (holdProgressUI != null)
            holdProgressUI.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(cameraPivot.position, cameraPivot.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.gameObject == this.gameObject) return;
            
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != currentLookTarget)
            {
                ClearOutline();
                ResetHold();
                
                currentLookTarget = hitObject;
                currentOutline = hitObject.GetComponent<Outline>();
                
                if (currentOutline != null)
                {
                    currentOutline.enabled = true;
                    
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
                    }
                    else if (GameObject.ReferenceEquals(hitObject, Chest))
                    {
                        currentChest = hitObject.GetComponent<Chest>();
                        if (currentChest != null && currentChest.CanInteract())
                        {
                            feedbackText.text = "Hold E to open Chest";
                        }
                        else
                        {
                            feedbackText.text = "Chest is already open";
                        }
                    }
                }
            }

            // Handle Chest hold interaction
            if (GameObject.ReferenceEquals(hitObject, Chest) && currentChest != null && currentChest.CanInteract())
            {
                if (Keyboard.current.eKey.isPressed)
                {
                    if (!isHolding)
                    {
                        isHolding = true;
                        holdTimer = 0f;
                        if (holdProgressUI != null)
                            holdProgressUI.SetActive(true);
                    }

                    holdTimer += Time.deltaTime;
                    
                    // Update progress bar
                    if (holdProgressBar != null)
                    {
                        holdProgressBar.fillAmount = holdTimer / requiredHoldTime;
                    }

                    if (holdTimer >= requiredHoldTime)
                    {
                        currentChest.OnHoldComplete();
                        ResetHold();
                    }
                }
                else if (isHolding)
                {
                    ResetHold();
                }
            }
            // Handle regular interactions
            else if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                IInteractable interactable = hitObject.GetComponent<IInteractable>();
                if (interactable != null && interactable.CanInteract())
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            ClearOutline();
            ResetHold();
        }
    }

    void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;
        if (holdProgressUI != null)
            holdProgressUI.SetActive(false);
        if (holdProgressBar != null)
            holdProgressBar.fillAmount = 0f;
        currentChest = null;
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