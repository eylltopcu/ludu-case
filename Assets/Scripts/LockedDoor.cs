using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;
    public bool isLocked = true;
    
    // Sound effects
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioClip unlockSound;
    public AudioClip lockedSound; // Sound when trying to open locked door
    private AudioSource audioSource;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private SimpleInventory playerInventory;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }

        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.maxDistance = 20f;

        // Find player inventory once
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<SimpleInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("SimpleInventory component not found on player!");
            }
        }
        else
        {
            Debug.LogError("No GameObject with 'Player' tag found!");
        }
    }

    public void Interact()
    {
        // If locked, try to unlock with key
        if (isLocked)
        {
            if (playerInventory != null && playerInventory.hasKey)
            {
                isLocked = false;
                playerInventory.hasKey = false;
                
                // Play unlock sound
                if (audioSource != null && unlockSound != null)
                {
                    audioSource.PlayOneShot(unlockSound);
                }
                
                Debug.Log("Door unlocked!");
                
                GameObject inventoryUI = GameObject.Find("InventoryUI");
                if (inventoryUI != null)
                    inventoryUI.SetActive(false);
                
                return;
            }
            else
            {
                // Play locked sound when trying to open without key
                if (audioSource != null && lockedSound != null)
                {
                    audioSource.PlayOneShot(lockedSound);
                }
                
                Debug.Log("Door is locked! Need a key.");
                return;
            }
        }

        // If unlocked, toggle door open/close
        isOpen = !isOpen;
        
        // Play appropriate sound
        if (audioSource != null)
        {
            if (isOpen && openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
            else if (!isOpen && closeSound != null)
            {
                audioSource.PlayOneShot(closeSound);
            }
        }
        
        Debug.Log(isOpen ? "Door opening!" : "Door closing!");
    }

    public bool CanInteract()
    {
        if (isLocked)
        {   
            if (playerInventory == null)
            {
                Debug.LogWarning("Player inventory not found!");
                return false;
            }
            return playerInventory.hasKey;
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