using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public float holdDuration = 2f;
    public GameObject chestLid;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        if (chestLid != null)
        {
            closedRotation = chestLid.transform.localRotation;
            openRotation = closedRotation * Quaternion.Euler(-openAngle, 0, 0);
        }

        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
    }

    public void Interact()
    {
        // This won't be called for hold interactions
        // But we keep it for interface compatibility
    }

    public bool CanInteract()
    {
        return !isOpen;
    }

    public void OnHoldComplete()
    {
        isOpen = true;
        Debug.Log("Chest opened!");
    }

    void Update()
    {
        if (isOpen && chestLid != null)
        {
            chestLid.transform.localRotation = Quaternion.Slerp(
                chestLid.transform.localRotation,
                openRotation,
                openSpeed * Time.deltaTime
            );
        }
    }
}