using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private InputAction movementAction;

    private Rigidbody rb;

    private void Start()
    {
        movementAction.Enable();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 movement = movementAction.ReadValue<Vector2>() * (movementSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + (transform.right * movement.x + transform.forward * movement.y));
    }
}
