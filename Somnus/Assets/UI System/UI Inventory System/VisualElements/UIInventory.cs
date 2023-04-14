using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIInventory : VisualElement
{
    private List<UIInventorySlot> Slots = new List<UIInventorySlot>();
    public UIInventory() 
    {
        AddToClassList("");

        InventoryChannel.ItemPickUpEvent += AddItem;
    } 
    void AddItem(Item item)
    {
        UIInventorySlot inventorySlot = new UIInventorySlot(item);
        Add(inventorySlot);
        Slots.Add(inventorySlot);
    }
    void RemoveItem(Item item)
    {
        int id = item.GetInstanceID();
        Slots.RemoveAt(Slots.FindIndex(t => t.id == id));
    }
}
