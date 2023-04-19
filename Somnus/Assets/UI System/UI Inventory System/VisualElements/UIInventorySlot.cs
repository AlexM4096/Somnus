using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIInventorySlot : Button
{
    public Item item;
    Image Icon;
    public UIInventorySlot()
    {
        focusable = true;
        AddToClassList("inventorySlot");

        Icon = new Image();
        Icon.AddToClassList("inventorySlotIcon");
        Add(Icon);
    }
    public UIInventorySlot(Item item) : this()
    {
        this.item = item;
        Icon.sprite = item.Icon;
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIInventorySlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
