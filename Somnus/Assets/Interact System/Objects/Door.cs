using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private bool IsClosed = true;

    //public override bool CanInteract() { return IsClosed; }

    protected override void DoInteractions()
    {
        SwitchState();
    }

    private void SwitchState()
    {
        IsClosed = !IsClosed;
        gameObject.SetActive(IsClosed);
    }   
}
