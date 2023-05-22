using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    float direction;

    Animator animator;
    Controls control;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        control = new();
    }

    private void OnEnable()
    {
        control.Player.Move.performed += _ => Move();
        control.Player.Move.canceled += _ => Move();
        control.Enable();

        DialogChannel.DialogStartEvent += _ => DisablePlayerControl();
        DialogChannel.DialogFinishEvent += _ => EnablePlayerControl();
    }

    private void OnDisable()
    {
        control.Player.Move.performed -= _ => Move();
        control.Player.Move.canceled -= _ => Move();
        control.Disable();

        DialogChannel.DialogStartEvent -= _ => DisablePlayerControl();
        DialogChannel.DialogFinishEvent -= _ => EnablePlayerControl();
    }

    private void DisablePlayerControl() { control.Player.Disable(); }
    private void EnablePlayerControl() { control.Player.Enable(); }

    private void Move()
    {
        direction = control.Player.Move.ReadValue<float>();
    }

    private void Update()
    {
        //if (direction != 0)
        //    animator.SetFloat("Speed", Mathf.Abs(direction));
        //else
        //    animator.SetFloat("Speed", 0);

        if (direction != 0)
        {       
            animator.Play("Walk");
        }
        else
            animator.Play("Idle");
    }
}
