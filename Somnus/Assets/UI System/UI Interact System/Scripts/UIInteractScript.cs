using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInteractScript : MonoBehaviour
{
    VisualElement Root;
    List<UIInteractText> Children;

    private void Start()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Root.AddToClassList("root");

        Children = new List<UIInteractText>();

        InteractableObject[] GameObjects = FindObjectsOfType<InteractableObject>();
        foreach (InteractableObject GameObject in GameObjects)
            AddUIInteractText(GameObject.InteractPivot);
    }
    private void Update()
    {
        foreach (UIInteractText Child in Children)
            Child.SetPosition();
    }
    void AddUIInteractText(Transform transform)
    {
        UIInteractText text = new(transform);
        Root.Add(text);
        Children.Add(text);
    }

    private void OnEnable()
    {
        DialogChannel.DialogStartEvent += DisplayOff;
        DialogChannel.DialogFinishEvent += DisplayOn;
    }

    private void OnDisable()
    {
        DialogChannel.DialogStartEvent -= DisplayOff;
        DialogChannel.DialogFinishEvent -= DisplayOn;
    }

    private void DisplayOn(Dialog dialog)
    {
        Root.style.display = DisplayStyle.Flex;
    }

    private void DisplayOff(Dialog dialog)
    {
        Root.style.display = DisplayStyle.None;
    }
}
