using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
        
        // Disable outline at start
        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void Interact()
    {
        isOpen = !isOpen;
        Debug.Log(isOpen ? "Door opening!" : "Door closing!");
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
    }

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }
}