public class Cabinet : InteractableObject
{
    bool IsSwaped = false;

    public override bool CanInteract()
    {
        return !IsSwaped;
    }

    protected override void DoInteractions()
    {
        UITransitionChannel.Blink();
        IsSwaped = !IsSwaped;
    }
}
