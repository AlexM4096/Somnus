using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIInventoryWindow : VisualElement
{
    private List<UIInventorySlot> Slots = new();
    public UIInventoryWindow()
    { 
        AddToClassList("inventoryWindow"); 
    }
    public void AddItem(Item item)
    {
        UIInventorySlot slot = new(item);

        Add(slot);
        Slots.Add(slot);
    }
    public bool RemoveItem(Item item)
    {
        foreach (var slot in Slots)
        {
            if (slot.Item == item)
            {
                Remove(slot);
                Slots.Remove(slot);

                return true;
            }
        }
        return false;
    }
    public void SetAllActiveFalse(UIInventorySlot slot)
    {
        foreach (var _slot in Slots)
        {
            if (!_slot.Equals(slot))
                _slot.SetActiveFalse();
        }
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIInventoryWindow, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
