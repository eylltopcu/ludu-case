using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public float holdDuration = 2f;
    public GameObject chestLid;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    
    // Sound effects
    public AudioClip openSound;
    public AudioClip creakSound; // Optional creaking sound while holding
    private AudioSource audioSource;
    
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
        audioSource.maxDistance = 15f;
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
        
        // Play open sound
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
        
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