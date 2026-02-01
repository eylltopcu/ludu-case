using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public GameObject objectToToggle; // Drag the object you want to turn on/off
    public bool isOn = false;

    void Start()
    {
        // Make sure object starts in correct state
        if (objectToToggle != null)
            objectToToggle.SetActive(isOn);
        
        // Disable outline at start
        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void Interact()
    {
        isOn = !isOn;
        
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(isOn);
            Debug.Log(isOn ? "Object activated!" : "Object deactivated!");
        }
        else
        {
            Debug.LogWarning("No object assigned to toggle!");
        }
    }

    public bool CanInteract()
    {
        return true; 
    }
}