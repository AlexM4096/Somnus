using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class UIInteractMark : VisualElement
{
    private Transform transformToFollow;
    private int id;
    public UIInteractMark()
    { 
        AddToClassList("interactMark"); 

        Image image = new Image();
        image.AddToClassList("interactMarkImage");
        Add(image);
    }
    public UIInteractMark(Transform tranform) : this()
    {

        transformToFollow = tranform;
        id = transformToFollow.GetInstanceID();

        UIInteractChannel.UIInteractShowEvent += Show;
        UIInteractChannel.UIInteractHideEvent += Hide;

        Hide(id);
    }
    public void SetPosition(Vector3 delta)
    {
        Vector3 newPosition = RuntimePanelUtils
            .CameraTransformWorldToPanel(panel, transformToFollow.position, Camera.main);
        transform.position = newPosition + delta;
    }
    private void Show(int id)
    {
        if (this.id != id) return;
        style.display = DisplayStyle.Flex;
    }
    private void Hide(int id)
    {
        if (this.id != id) return;
        style.display = DisplayStyle.None;
    }
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<UIInteractMark, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}
