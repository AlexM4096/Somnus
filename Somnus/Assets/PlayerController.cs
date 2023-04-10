using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float direction;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();

        controls.Main.Interact.performed += Interact;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        direction = controls.Main.Move.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * speed, 0);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Ok");    
    }
}
