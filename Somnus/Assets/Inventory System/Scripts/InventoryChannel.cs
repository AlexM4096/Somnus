using System;

static public class InventoryChannel
{
    static public Action<Item> ItemPickUpEvent;

    static public void PickUpItem(Item item)
    {
        ItemPickUpEvent?.Invoke(item);
    }
}
