using UnityEngine;
using UnityEngine.UIElements;

public class UIDialogScript : MonoBehaviour
{
    VisualElement _root;
    UIDialogWindow _dialogWindow;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _dialogWindow = new UIDialogWindow();
        _root.Add(_dialogWindow);

        _root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        DialogChannel.DialogStartEvent += DisplayOn;
        DialogChannel.DialogFinishEvent += DisplayOff;
    }

    private void OnDisable()
    {
        DialogChannel.DialogStartEvent -= DisplayOn;
        DialogChannel.DialogFinishEvent -= DisplayOff;
    }

    private void DisplayOn(Dialog dialog)
    {
        _dialogWindow.Clear();
        _dialogWindow.Update(dialog);

        _root.style.display = DisplayStyle.Flex;
    }

    private void DisplayOff(Dialog dialog)
    {
        _root.style.display = DisplayStyle.None;
    }
}
