using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInteractScript : MonoBehaviour
{
    VisualElement Root;
    List<UIInteractMark> Children;

    [SerializeField] float Speed = 0.1f;
    [SerializeField] float Length = 1f;
    float x; Vector3 delta;

    private void Start()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Root.AddToClassList("root");

        Children = new List<UIInteractMark>();

        InteractableObject[] GameObjects = FindObjectsOfType<InteractableObject>();
        foreach (InteractableObject GameObject in GameObjects)
            AddUIInteractText(GameObject.InteractPivot);
    }
    private void FixedUpdate()
    {
        delta = new(0, Length * Mathf.Sin(x += Speed), 0);
        foreach (UIInteractMark Child in Children)
        {          
            Child.SetPosition(delta);
        }          
    }
    void AddUIInteractText(Transform transform)
    {
        UIInteractMark text = new(transform);
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
