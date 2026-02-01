using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class KeyPickup : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public GameObject InventoryUI;

    bool playerInRange = false;
    SimpleInventory playerInventory;
        void Start()
    {

        Outline outline = GetComponent<Outline>();
        if (outline != null)
            outline.enabled = false;
        InventoryUI.SetActive(false);
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
            Debug.Log("Key alındı!");
            Destroy(gameObject);
            feedbackText.text = "Key collected!";
            playerInventory.hasKey = true;
        }
        if (playerInventory.hasKey==true)
        {
              InventoryUI.SetActive(true);
        }
    }
}
