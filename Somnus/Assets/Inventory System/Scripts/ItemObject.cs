using UnityEngine;

public class ItemObject : InteractableObject
{
    [SerializeField] private Item item;
    private bool IsPicked = false;

    public override bool CanInteract() { return !IsPicked; }
    public override void StartInteract()
    { 
        base.StartInteract();
        if (CanInteract()) PickUp(); 
    }
    private void PickUp()
    {
        IsPicked = true;
        gameObject.SetActive(false);
        InventoryChannel.AddItem(item);
    }
}
