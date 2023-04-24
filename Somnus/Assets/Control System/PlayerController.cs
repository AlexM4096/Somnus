using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 5;
    private float Direction;

    private Rigidbody2D RB;
    private Controls Control;

    HashSet<InteractableObject> Interactables;
    private Item ChoosedItem;

    private void Awake()
    {
        Control = new Controls();
        Interactables = new();

        RB = GetComponent<Rigidbody2D>();     
    }

    private void OnEnable()
    {
        Control.Player.Click.performed += ClickToDo;
        Control.Enable();

        DialogChannel.DialogStartEvent += DisablePlayerControl;
        DialogChannel.DialogFinishEvent += EnablePlayerControl;

        InventoryChannel.ItemChooseEvent += SetChoosedItem;
    }

    private void OnDisable()
    {
        Control.Player.Click.performed -= ClickToDo;
        Control.Disable();

        DialogChannel.DialogStartEvent -= DisablePlayerControl;
        DialogChannel.DialogFinishEvent -= EnablePlayerControl;

        InventoryChannel.ItemChooseEvent -= SetChoosedItem;
    }

    private void DisablePlayerControl(Dialog dialog) { Control.Player.Disable(); }
    private void EnablePlayerControl(Dialog dialog) { Control.Player.Enable(); }

    private void SetChoosedItem(Item item) { ChoosedItem = item; }

    private void Update()
    {
        Direction = Control.Player.Move.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InteractableObject>(out var interactable))
        {
            interactable.SetActive(true);
            Interactables.Add(interactable);
        }                 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InteractableObject>(out var interactable))
        {
            interactable.SetActive(false);
            Interactables.Remove(interactable);
        }          
    }

    private void Move()
    {
        RB.velocity = Direction * Speed * Vector2.right;
    }

    private void ClickToDo(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Control.Player.Position.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

        TryInteract(hits);
        TryDrag(hits);
    }

    private void TryInteract(RaycastHit2D[] hits)
    {    
        if (DetectInteractable(hits, out var interactable))
            interactable.StartInteract(ChoosedItem);        
    }

    private bool DetectInteractable(RaycastHit2D[] hits, out InteractableObject interactable)
    {
        interactable = null;
        foreach (var hit in hits)
        {
            if (!hit || hit.collider == null || hit.collider.isTrigger) continue;
            if (hit.collider.TryGetComponent(out interactable))
                return Interactables.Contains(interactable);
        }
        return false;
    }

    private void TryDrag(RaycastHit2D[] hits)
    {
        if (DetectDragable(hits, out var dragable))
            dragable.SwitchState();
    }

    private bool DetectDragable(RaycastHit2D[] hits, out DragableObject dragable)
    {
        dragable = null;
        foreach (var hit in hits)
        {
            if (!hit || hit.collider == null) continue;
            if (hit.collider.TryGetComponent(out dragable))
                return true;
        }
        return false;
    }
}
