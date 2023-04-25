using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIInventorySlot : Button
{
    public bool Active;

    public Item Item;
    Image Icon;   

    public UIInventorySlot()
    {
        AddToClassList("inventorySlot");

        Icon = new Image();
        Icon.AddToClassList("inventorySlotIcon");
        Add(Icon);

        Active = false;
        clicked += SwitchState;
    }

    public UIInventorySlot(Item item) : this()
    {
        Item = item;
        Icon.sprite = item.Icon;
    }

    private void SwitchState()
    {
        Active = !Active;

        if (Active)
        {
            AddToClassList("inventorySlot_active");
            UIInventoryChannel.ChooseSlot(this);
            InventoryChannel.ChooseItem(Item);
        }
        else
        {
            RemoveFromClassList("inventorySlot_active");
            UIInventoryChannel.ChooseSlot(null);
            InventoryChannel.ChooseItem(null);
        }           
    }
    public void SetActiveFalse()
    { 
        Active = false;
        RemoveFromClassList("inventorySlot_active");
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIInventorySlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
