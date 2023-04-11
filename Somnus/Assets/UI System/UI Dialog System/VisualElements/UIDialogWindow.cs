using System.Collections.Generic;
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

        List<DialogChoice> choices = dialog.Choices; 
        for (int index = 0; index < choices.Count; index++)
        {
            UIDialogChoice dialogChoice = new UIDialogChoice(dialog, choices[index]);
            Add(dialogChoice);
        }
    }
}
