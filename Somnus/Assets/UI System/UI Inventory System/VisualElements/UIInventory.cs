using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIInventory : VisualElement
{
    private List<UIInventorySlot> Slots = new List<UIInventorySlot>();
    public UIInventory()
    { 
        focusable = true;
        AddToClassList("inventoryWindow"); 
    } 
    public void AddItem(Item item)
    {
        UIInventorySlot slot = new UIInventorySlot(item);
        Slots.Add(slot);
        Add(slot);
    }
    public void RemoveItem(Item item)
    {
        int index = Slots.FindIndex(t => t.item == item);
        if (index != -1)
        {
            UIInventorySlot slot = Slots[index];
            Remove(slot);
            Slots.RemoveAt(index);
        }
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIInventory, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
