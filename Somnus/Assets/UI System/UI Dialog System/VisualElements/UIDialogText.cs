using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIDialogText : Label
{ 
    public UIDialogText(Dialog dialog)
    {
        AddToClassList("dialogText");

        text = dialog.NPCText;
    }
}   