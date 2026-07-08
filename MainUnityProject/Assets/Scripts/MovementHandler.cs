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
        Vector2 movement = movementAction.ReadValue<Vector2>();
        movement.Normalize();
        Vector3 vec = new Vector3(movement.x, 0, movement.y);

        vec *= movementSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + vec);
    }
}
