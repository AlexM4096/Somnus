using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 5;
    private float Direction;
    private bool IsFacingRight = true;
    private Vector3 FlipVector = new(0, 180, 0);

    private Rigidbody2D RB;
    private Controls Control;

    HashSet<InteractableObject> Interactables;
    private Item ChoosedItem;

    private void Awake()
    {
        Control = new();
        Interactables = new();

        RB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Control.Player.Move.started += Move;
        Control.Player.Move.canceled += Move;
        Control.Player.Click.performed += ClickToDo;
        Control.Enable();

        DialogChannel.DialogStartEvent += _ => DisablePlayerControl();
        DialogChannel.DialogFinishEvent += _ => EnablePlayerControl();

        InventoryChannel.ItemChooseEvent += SetChoosedItem;
    }

    private void OnDisable()
    {
        Control.Player.Move.started -= Move;
        Control.Player.Move.canceled -= Move;
        Control.Player.Click.performed -= ClickToDo;
        Control.Disable();

        DialogChannel.DialogStartEvent -= _ => DisablePlayerControl();
        DialogChannel.DialogFinishEvent -= _ => EnablePlayerControl();

        InventoryChannel.ItemChooseEvent -= SetChoosedItem;
    }

    private void DisablePlayerControl() { Control.Player.Disable(); }
    private void EnablePlayerControl() { Control.Player.Enable(); }

    private void SetChoosedItem(Item item) { ChoosedItem = item; }

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

    private void Move(InputAction.CallbackContext context)
    {
        Direction = Control.Player.Move.ReadValue<float>();       
        RB.velocity = Direction * Speed * Vector2.right;

        if (Direction > 0 && !IsFacingRight)
            Flip();
        if (Direction < 0 && IsFacingRight)
            Flip();
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
            //if (!hit || hit.collider == null || hit.collider.isTrigger) continue;
            if (!hit || hit.collider == null) continue;
            if (hit.collider.TryGetComponent(out interactable))
                return Interactables.Contains(interactable);
        }
        return false;
    }

    private void TryDrag(RaycastHit2D[] hits)
    {
        if (DetectDragable(hits, out var dragable))
            dragable.Drag();
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

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        transform.Rotate(FlipVector);
    }
}
