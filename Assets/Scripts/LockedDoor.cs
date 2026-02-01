using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    public bool isLocked = true;   // 🔒 NEW

    private Quaternion closedRotation;
    private Quaternion openRotation;

    SimpleInventory playerInventory;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;

        // find player inventory once
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerInventory = player.GetComponent<SimpleInventory>();
    }

    public void Interact()
    {
        if (!CanInteract())
        {
            Debug.Log("Door is locked! You need a key.");
            return;
        }

        isOpen = !isOpen;
        Debug.Log(isOpen ? "Door opening!" : "Door closing!");
    }

    public bool CanInteract()
    {
        // if door is locked, check key
        if (isLocked)
        {   
            Debug.Log("Checking for key in inventory..."+playerInventory.hasKey);
            return playerInventory != null && playerInventory.hasKey;
        }
        return true;
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            openSpeed * Time.deltaTime
        );
    }
}
