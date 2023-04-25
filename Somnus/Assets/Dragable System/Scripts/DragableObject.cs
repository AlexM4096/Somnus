using UnityEngine;

public class DragableObject : MonoBehaviour
{
    [SerializeField] private bool IsDraging = false;
    [SerializeField] private bool IsHolding = false;

    private Controls Control;

    Vector2 MousePostion = Vector2.zero;
    Vector2 Difference = Vector2.zero;

    private void Awake()
    {
        Control = new Controls();
    }

    private void OnEnable()
    {
        Control.Player.Click.started += _ => SetHolding(true);
        Control.Player.Click.canceled += _ => SetHolding(false);
        Control.Player.Click.canceled += _ => SetDraging(false);

        Control.Enable();
    }

    private void OnDisable()
    {
        Control.Player.Click.started -= _ => SetHolding(true);
        Control.Player.Click.canceled -= _ => SetHolding(false);
        Control.Player.Click.canceled -= _ => SetDraging(false);

        Control.Disable();
    }

    private void SetHolding(bool state) { IsHolding = state; }
    private void SetDraging(bool state) { IsDraging = state; }

    private void Update()
    {
        GetMousePosition();
    }

    private void FixedUpdate()
    {
        SetTransform();
    }

    public void Drag()
    {
        SetDraging(true);
        CalculateDifference();
    }

    private void CalculateDifference()
    {
        MousePostion = (Vector2)Camera.main.ScreenToWorldPoint(Control.Player.Position.ReadValue<Vector2>());
        Difference = MousePostion - (Vector2)transform.position;
    }

    private bool Check()
    {
        return IsDraging && IsHolding;
    }

    private void GetMousePosition()
    {
        if (!Check()) return;
        Vector2 localPos = Control.Player.Position.ReadValue<Vector2>();
        MousePostion = (Vector2)Camera.main.ScreenToWorldPoint(localPos);
        MousePostion -= Difference;
    }

    private void SetTransform()
    {
        if (!Check()) return;
        transform.position = Vector2.Lerp(transform.position, MousePostion, 0.5f);
    }
}