using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogWindow : VisualElement
{
    public UIDialogWindow() { AddToClassList("dialogWindow"); }

    public void Update(Dialog dialog)
    {
        Add(new UIDialogText(dialog));

        foreach (DialogChoice dialogChoice in dialog.Choices)
            Add(new UIDialogChoice(dialog, dialogChoice));
    }

    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIDialogWindow, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}