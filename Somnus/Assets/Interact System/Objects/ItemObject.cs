using UnityEngine;

public class ItemObject : InteractableObject
{
    [SerializeField] private Item ItemData;
    private bool IsPicked = false;

    public override bool CanInteract() { return !IsPicked; }

    protected override void DoInteractions()
    {
        IsPicked = true;
        gameObject.SetActive(false);
        InventoryChannel.AddItem(ItemData);
    }
}
