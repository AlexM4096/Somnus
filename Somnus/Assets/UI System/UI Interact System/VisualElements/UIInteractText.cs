using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIInteractText : TextElement
{
    private Transform transformToFollow;
    private int id;
    public UIInteractText() { AddToClassList("interactText"); }
    public UIInteractText(Transform tranform) : this()
    {
        transformToFollow = tranform;
        id = transformToFollow.GetInstanceID();

        UIInteractChannel.UIInteractShowEvent += Show;
        UIInteractChannel.UIInteractHideEvent += Hide;

        //InteractChannel.InteractStartEvent += Hide;
        //InteractChannel.InteractFinishEvent += Show;

        Hide(id);
    }
    public void SetPosition()
    {
        if (panel == null) return;
        Vector3 newPosition = RuntimePanelUtils
            .CameraTransformWorldToPanel(panel, transformToFollow.position, Camera.main);
        transform.position = newPosition;
    }
    private void Show(int id)
    {
        if (this.id != id) return;
        style.display = DisplayStyle.Flex;
    }
    private void Show(InteractableObject interactable)
    {
        id = interactable.GetInstanceID();
        Show(id);
    }
    private void Hide(int id)
    {
        if (this.id != id) return;
        style.display = DisplayStyle.None;
    }
    private void Hide(InteractableObject interactable)
    {
        id = interactable.GetInstanceID();
        Hide(id);
    }
    //private void TryHide(int id)
    //{
    //    if (this.id != id) return;
    //}
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIInteractText, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
