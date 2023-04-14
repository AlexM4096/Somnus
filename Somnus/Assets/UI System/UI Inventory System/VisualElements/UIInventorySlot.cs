using UnityEngine;
using UnityEngine.UIElements;

public class UIInventorySlot : VisualElement
{
    public int id;
    Image Icon = null;
    public UIInventorySlot() { AddToClassList(""); }
    public UIInventorySlot(Item item)
    {
        id = item.GetInstanceID();

        Icon = new Image();
        Icon.sprite = item.GetComponent<Sprite>();
    }
}
