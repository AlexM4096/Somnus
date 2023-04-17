using System.Collections.Generic;
using UnityEngine.UIElements;

public class UIInventory : VisualElement
{
    private List<UIInventorySlot> Slots = new List<UIInventorySlot>();
    public UIInventory() 
    {
        AddToClassList("");
    } 
    void AddItem(Item item)
    {
    }
    void RemoveItem(Item item)
    {
    }
}
