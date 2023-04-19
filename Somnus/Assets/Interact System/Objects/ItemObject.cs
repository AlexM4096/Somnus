using UnityEngine;

public class ItemObject : InteractableObject
{
    [SerializeField] private Item ItemPrefab;
    Item item;
    private bool IsPicked = false;
    protected override void Awake()
    {
        base.Awake();
        item = Instantiate(ItemPrefab);
    }
    public override bool CanInteract() { return !IsPicked; }
    public override void StartInteract()
    { 
        base.StartInteract();
        if (!CanInteract()) return;
        PickUp(); 
    }
    private void PickUp()
    {
        IsPicked = true;
        gameObject.SetActive(false);
        InventoryChannel.AddItem(item);
    }
}
