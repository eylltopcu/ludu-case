using UnityEngine;
public class Item : MonoBehaviour, IInteractable
{
    public bool CanInteract()
    {
        return true; // burada şart koyabilirsin
    }

    public void Interact()
    {
        Debug.Log("Item alındı");
    }
}
