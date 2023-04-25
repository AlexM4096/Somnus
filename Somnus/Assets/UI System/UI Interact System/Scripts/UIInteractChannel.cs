using System;

static public class UIInteractChannel
{
    static public Action<int> UIInteractShowEvent;
    static public Action<int> UIInteractHideEvent;
    static public void ShowUIInteract(int id)
    {
        UIInteractShowEvent?.Invoke(id);
    }
    static public void HideUIInteract(int id)
    {
        UIInteractHideEvent?.Invoke(id);
    }
}
