using UnityEngine;
using UnityEngine.InputSystem;

public class DragableObject : MonoBehaviour
{
    [SerializeField] private bool IsDraging = false;
    private Controls Control;
    Vector2 MousePostion = Vector2.zero;
    Vector2 Difference = Vector2.zero;

    private void Awake()
    {
        Control = new Controls();
    }

    private void OnEnable()
    {       
        Control.Enable();
    }

    private void OnDisable()
    {
        Control.Disable();
    }

    private void Update()
    {
        if (IsDraging)
            MousePostion = (Vector2)Camera.main.ScreenToWorldPoint(Control.Player.Position.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        if (IsDraging)
            transform.position = Vector2.Lerp(transform.position, MousePostion - Difference, 0.5f);
    }
        
    public void SwitchState()
    {
        IsDraging = !IsDraging;

        if (IsDraging)         
            CalculateDifference();
    }

    private void CalculateDifference()
    {
        MousePostion = (Vector2)Camera.main.ScreenToWorldPoint(Control.Player.Position.ReadValue<Vector2>());
        Difference = MousePostion - (Vector2)transform.position;
    }
}
