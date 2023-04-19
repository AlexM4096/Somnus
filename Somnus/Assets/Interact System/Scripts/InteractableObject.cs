using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public Transform InteractPivot;
    protected virtual void Awake()
    {
        InteractPivot = transform.Find("InteractPivot").transform;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!CanInteract()) return;
        UIInteractChannel.ShowUIInteract(InteractPivot.GetInstanceID());
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        UIInteractChannel.HideUIInteract(InteractPivot.GetInstanceID());
    }
    abstract public bool CanInteract();
    virtual public void StartInteract()
    {      
        InteractChannel.StartInteract(this);
    }
    virtual public void FinishInteract()
    {
        InteractChannel.FinishInteract(this);
    }
}
