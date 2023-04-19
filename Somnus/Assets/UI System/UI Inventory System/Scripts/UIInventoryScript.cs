using UnityEngine;
using UnityEngine.UIElements;

public class UIInventoryScript : MonoBehaviour
{
    VisualElement Root;
    UIInventory InventoryWindow;

    private void Awake()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Root.Clear();

        Root.AddToClassList("root");

        InventoryWindow = new UIInventory();
        Root.Add(InventoryWindow);

        //Root.style.display = DisplayStyle.None;
    }
    private void OnEnable()
    {
        InventoryChannel.ItemAddEvent += AddItem;
        InventoryChannel.ItemRemoveEvent += RemoveItem;

        UIInventoryChannel.UIInventoryDisplayOnEvent += DisplayOn;
        UIInventoryChannel.UIInventoryDisplayOffEvent += DisplayOff;
    }
    private void OnDisable()
    {
        InventoryChannel.ItemAddEvent -= AddItem;
        InventoryChannel.ItemRemoveEvent -= RemoveItem;

        UIInventoryChannel.UIInventoryDisplayOnEvent -= DisplayOn;
        UIInventoryChannel.UIInventoryDisplayOffEvent -= DisplayOff;
    }
    private void DisplayOn()
    {
        Root.style.display = DisplayStyle.Flex;
    }

    private void DisplayOff()
    {
        Root.style.display = DisplayStyle.None;
    }
    private void AddItem(Item item)
    {
        InventoryWindow.AddItem(item);
    }
    private void RemoveItem(Item item)
    {
        InventoryWindow.RemoveItem(item);
    }
}
