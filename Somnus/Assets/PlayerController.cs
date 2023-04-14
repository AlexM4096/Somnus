using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float direction;
    private Controls controls;

    private InteractableObject interactable;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();

        controls.Player.Interact.performed += Interact;
    }

    private void OnEnable()
    {
        DialogChannel.DialogStartEvent += DisablePlayerControl;
        DialogChannel.DialogFinishEvent += EnablePlayerControl;

        controls.Enable();
    }

    private void OnDisable()
    {
        DialogChannel.DialogStartEvent -= DisablePlayerControl;
        DialogChannel.DialogFinishEvent -= EnablePlayerControl;

        controls.Disable();
    }

    private void DisablePlayerControl(Dialog dialog) { controls.Player.Disable(); }
    private void EnablePlayerControl(Dialog dialog) { controls.Player.Enable(); }

    private void Update()
    {
        direction = controls.Player.Move.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<InteractableObject>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = null;
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * speed, 0);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log(controls.Player.StartInteract.activeControl.name);
        if (interactable == null) return;
        if (interactable.CanInteract()) interactable.StartInteract();
    }

    private float DistanceToMe(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
    }
}
