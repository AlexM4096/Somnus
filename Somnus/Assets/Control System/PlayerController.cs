using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody2D Rb;
    private float Direction;
    private Controls Control;

    private List<InteractableObject> Interactables;

    private void Awake()
    {
        Control = new Controls();
        Interactables = new List<InteractableObject>();

        Rb = GetComponent<Rigidbody2D>();     
    }

    private void OnEnable()
    {
        Control.Player.Interact.performed += Interact;

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
        Rb.velocity = Direction * Speed * Vector2.right;
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
}
