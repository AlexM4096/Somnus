using UnityEngine;
using UnityEngine.UIElements;

public class UIInventoryScript : MonoBehaviour
{
    VisualElement Root;
    UIInventoryWindow InventoryWindow;

    private void Awake()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Root.Clear();

        Root.AddToClassList("root");

        InventoryWindow = new UIInventoryWindow();
        Root.Add(InventoryWindow);

        Root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        UIInventoryChannel.UIInventoryDisplayOnEvent += DisplayOn;
        UIInventoryChannel.UIInventoryDisplayOffEvent += DisplayOff;

        UIInventoryChannel.UIInventorySlotChooseEvent += ChooseSlot;

        InventoryChannel.ItemAddEvent += AddItem;
        InventoryChannel.ItemRemoveEvent += RemoveItem;
        //InventoryChannel.ItemUseEvent += UseItem;
    }

    private void OnDisable()
    {
        UIInventoryChannel.UIInventoryDisplayOnEvent -= DisplayOn;
        UIInventoryChannel.UIInventoryDisplayOffEvent -= DisplayOff;

        UIInventoryChannel.UIInventorySlotChooseEvent -= ChooseSlot;

        InventoryChannel.ItemAddEvent -= AddItem;
        InventoryChannel.ItemRemoveEvent -= RemoveItem;
        //InventoryChannel.ItemUseEvent -= UseItem;
    }


    private void DisplayOn() { Root.style.display = DisplayStyle.Flex; }
    private void DisplayOff() { Root.style.display = DisplayStyle.None; }

    private void AddItem(Item item) { InventoryWindow.AddItem(item); }
    private void RemoveItem(Item item) { InventoryWindow.RemoveItem(item); }

    private void ChooseSlot(UIInventorySlot slot) 
    { 
        InventoryWindow.SetAllActiveFalse(slot); 
    }
}
