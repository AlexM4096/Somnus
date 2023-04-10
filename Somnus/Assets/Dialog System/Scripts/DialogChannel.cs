using System;

public class DialogChannel
{
    public Action<Dialog> DialogStartEvent;
    public Action<Dialog> DialogFinishEvent;

    public void StartDialog(Dialog dialog)
    {
        DialogStartEvent?.Invoke(dialog);
    }

    public void FinishDialog(Dialog dialog)
    {
        DialogFinishEvent?.Invoke(dialog);
    }
}
