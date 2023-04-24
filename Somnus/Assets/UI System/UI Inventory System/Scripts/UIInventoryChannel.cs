using System;

static public class UIInventoryChannel
{
    static public Action UIInventoryDisplayOnEvent;
    static public Action UIInventoryDisplayOffEvent;

    static public Action<UIInventorySlot> UIInventorySlotChooseEvent;

    static public void DisplayOn()
    {
        UIInventoryDisplayOnEvent?.Invoke();
    }
    static public void DisplayOff()
    {
        UIInventoryDisplayOffEvent?.Invoke();
    }

    static public void ChooseSlot(UIInventorySlot slot)
    {
        UIInventorySlotChooseEvent?.Invoke(slot);
    }
}
