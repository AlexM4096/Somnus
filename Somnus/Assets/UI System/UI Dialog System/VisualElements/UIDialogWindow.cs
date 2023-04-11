using UnityEngine.UIElements;

public class UIDialogWindow : VisualElement
{
    public UIDialogWindow()
    {
        AddToClassList("dialogWindow");
    }

    public void Update(Dialog dialog)
    {
        Add(new UIDialogText(dialog));

        foreach (DialogChoice dialogChoice in dialog.Choices)
            Add(new UIDialogChoice(dialog, dialogChoice));
    }
}
