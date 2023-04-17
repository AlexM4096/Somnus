using UnityEngine;
using UnityEngine.UIElements;

public class UIInventorySlot : VisualElement
{
    Image Icon;
    public UIInventorySlot() { AddToClassList(""); }
    public UIInventorySlot(Item item)
    {
        Icon.sprite = item.Icon;
    }
}
