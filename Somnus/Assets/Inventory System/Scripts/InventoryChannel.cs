using System;

static public class InventoryChannel
{
    static public Action<Item> ItemAddEvent;
    static public Action<Item> ItemRemoveEvent;
    static public Action<Item> ItemUseEvent;

    static public void AddItem(Item item)
    {
        ItemAddEvent?.Invoke(item);
    }
    static public void RemoveItem(Item item)
    {
        ItemRemoveEvent?.Invoke(item);
    }
    static public void UseItem(Item item)
    {
        ItemUseEvent?.Invoke(item);
    }
}
