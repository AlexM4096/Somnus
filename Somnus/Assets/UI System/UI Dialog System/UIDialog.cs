

public class UIDialog
{
    DialogChannel _dialogChannel;

    private void Enable()
    {
        _dialogChannel.DialogStartEvent += DisplayOn;
        _dialogChannel.DialogFinishEvent += DisplayOff;
    }

    private void Disable()
    {
        _dialogChannel.DialogStartEvent -= DisplayOn;
        _dialogChannel.DialogFinishEvent -= DisplayOff;
    }

    private void DisplayOn(Dialog dialog)
    {

    }

    private void DisplayOff(Dialog dialog)
    {

    }
}
