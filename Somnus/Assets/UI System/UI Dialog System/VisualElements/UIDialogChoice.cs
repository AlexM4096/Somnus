using UnityEngine.UIElements;

public class UIDialogChoice : Button
{
    public UIDialogChoice(Dialog dialog, DialogChoice dialogChoice)
    {
        focusable = true;
        AddToClassList("dialogChoice");
        text = dialogChoice.PlayerText;
        Dialog nextDialog = dialogChoice.NextDialog;
        clicked += (nextDialog != null) ? nextDialog.StartDialog : dialog.FinishDialog;
    }
}
