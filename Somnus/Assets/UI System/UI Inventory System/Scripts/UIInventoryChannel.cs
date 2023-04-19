using System;

static public class UIInventoryChannel
{
    static public Action UIInventoryDisplayOnEvent;
    static public Action UIInventoryDisplayOffEvent;

    static public void DisplayOn()
    {
        UIInventoryDisplayOnEvent?.Invoke();
    }

    static public void DisplayOff()
    {
        UIInventoryDisplayOffEvent?.Invoke();
    }
}
