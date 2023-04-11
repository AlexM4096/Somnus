using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private bool IsPicked = false;
    public bool CanInteract() { return !IsPicked; }
    public void Interact() { if (CanInteract()) PickUp(); }
    private void PickUp()
    {
        IsPicked = true;
        gameObject.SetActive(false);
        InventoryChannel.PickUpItem(this);
    }
}
