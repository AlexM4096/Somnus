using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float direction;
    private Controls controls;
    private Animator anim;
    private SpriteRenderer sr;

    private IInteractable interactable;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

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
        Reflect();

    }
    public bool faceRight = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = null;
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * speed, 0);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }
    void Reflect()
    {
        if (rb.velocity.x > 0 && !faceRight || (rb.velocity.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactable == null) return;
        if (interactable.CanInteract()) interactable.Interact();
    }

    private float DistanceToMe(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
    }
}
