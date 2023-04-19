using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 5;

    private Rigidbody2D RB;
    private float Direction;
    private Controls Control;

    private List<InteractableObject> Interactables;

    private void Awake()
    {
        Control = new Controls();
        Interactables = new List<InteractableObject>();

        RB = GetComponent<Rigidbody2D>();     
    }

    private void OnEnable()
    {
        Control.Player.Interact.performed += Interact;

        Control.Player.Click.performed += _ => DetectObject();

        DialogChannel.DialogStartEvent += DisablePlayerControl;
        DialogChannel.DialogFinishEvent += EnablePlayerControl;

        Control.Enable();
    }

    private void OnDisable()
    {
        Control.Player.Interact.performed -= Interact;

        DialogChannel.DialogStartEvent -= DisablePlayerControl;
        DialogChannel.DialogFinishEvent -= EnablePlayerControl;

        Control.Disable();
    }

    private void DisablePlayerControl(Dialog dialog) { Control.Player.Disable(); }
    private void EnablePlayerControl(Dialog dialog) { Control.Player.Enable(); }

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
            Interactables.Add(interactable);         
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<InteractableObject>(out var interactable))
            Interactables.Remove(interactable);
    }

    private void Move()
    {
        RB.velocity = Direction * Speed * Vector2.right;
    }

    public void Interact(InputAction.CallbackContext context)
    { 
        if (Interactables.Count == 0) return;

        InteractableObject closest = Interactables[0];
        float distance, minDistance = DistanceToPlayer(closest);

        foreach (var interactable in Interactables)
        {
            distance = DistanceToPlayer(interactable);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = interactable;
            }
        }

        closest.StartInteract();
    }

    private float DistanceToPlayer(InteractableObject interactable)
    {
        Vector3 position = interactable.transform.position;
        return Vector3.Distance(transform.position, position);
    }

    private void DetectObject()
    {
        Vector2 mousePosition = Control.Player.Position.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit && hit.collider != null)
        {
            hit.collider.TryGetComponent<InteractableObject>(out InteractableObject interactable);
            if (Interactables.Contains(interactable)) interactable.StartInteract();
        }
    }
}
