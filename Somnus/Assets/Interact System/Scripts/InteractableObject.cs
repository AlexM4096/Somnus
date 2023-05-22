using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected Item RequiredItem = null;
    public Transform InteractPivot;
    private int PivotID;

    protected virtual void Awake()
    {
        InteractPivot = transform.Find("InteractPivot").transform;
        PivotID = InteractPivot.GetInstanceID();
    }

    public void SetActive(bool InRadius)
    {
        if (InRadius && CanInteract())
            UIInteractChannel.ShowUIInteract(PivotID);
        else
            UIInteractChannel.HideUIInteract(PivotID);
    }

    public virtual bool CanInteract() { return true; }

    private bool CheckRequirement(Item item)
    {
        return NullRequirment() || (RequiredItem == item);
    }

    private bool NullRequirment()
    {
        return RequiredItem == null;
    }

    private bool Check(Item item)
    {
        return CanInteract() && CheckRequirement(item);
    }

    protected abstract void DoInteractions();

    public void StartInteract(Item item)
    {
        if (Check(item))
        {
            InteractChannel.StartInteract(this);
            UIInteractChannel.HideUIInteract(PivotID);
            DoInteractions();

            if (!NullRequirment())
            {
                InventoryChannel.RemoveItem(item);
                //InventoryChannel.UseItem(item);               
            }

            FinishInteract();
        }   
    }

    public void FinishInteract()
    {
        InteractChannel.FinishInteract(this);
        if (CanInteract()) UIInteractChannel.ShowUIInteract(PivotID);
    }
}
