using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogChoice : Button
{
    public UIDialogChoice()
    {
        focusable = true;
        AddToClassList("dialogChoice");
    }

    public UIDialogChoice(Dialog dialog, DialogChoice dialogChoice) : this ()
    {
        text = dialogChoice.PlayerText;
        Dialog nextDialog = dialogChoice.NextDialog;
        clicked += (nextDialog != null) ? nextDialog.StartDialog : dialog.FinishDialog;
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIDialogChoice, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
