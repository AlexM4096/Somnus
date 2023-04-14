using System;

static public class InteractChannel
{
    static public Action<InteractableObject> InteractStartEvent;
    static public Action<InteractableObject> InteractFinishEvent;

    static public void StartInteract(InteractableObject interactable)
    {
        InteractStartEvent?.Invoke(interactable);
    }
    static public void FinishInteract(InteractableObject interactable)
    {
        InteractFinishEvent?.Invoke(interactable);
    }
}
