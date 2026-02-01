using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class KeyPickup : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public GameObject InventoryUI;
    
    // Sound effect
    public AudioClip pickupSound;
    private AudioSource audioSource;

    bool playerInRange = false;
    SimpleInventory playerInventory;
    
    void Start()
    {
        Outline outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
        
        InventoryUI.SetActive(false);
        
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.maxDistance = 15f;
    }
    
    void OnTriggerEnter(Collider other)
    {
        SimpleInventory inv = other.GetComponentInParent<SimpleInventory>();
        if (inv == null) return;

        playerInRange = true;
        playerInventory = inv;

        feedbackText.text = "Press E to pick up Key";
        feedbackText.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        SimpleInventory inv = other.GetComponentInParent<SimpleInventory>();
        if (inv == null) return;

        playerInRange = false;
        playerInventory = null;

        feedbackText.text = " ";
    }

    void Update()
    {
        if (!playerInRange) return;

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            playerInventory.hasKey = true;
            Debug.Log("Key picked up!");
            
            // Play pickup sound
            if (pickupSound != null)
            {
                // Play sound at the key's position before destroying it
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }
            
            feedbackText.text = "Key collected!";
            InventoryUI.SetActive(true);
            
            Destroy(gameObject);
        }
    }
}