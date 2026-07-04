using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] private InputAction movementAction;
    //[SerializeField] InputAction rotationAction;

    private Rigidbody rigidBody;

    private void OnEnable()
    {
        movementAction.Enable();
        //rotationAction.Enable();
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMove();
        //HandleRotate();
    }

    //private void HandleRotate() => rigidBody.angularVelocity = transform.up * rotationSpeed * rotationAction.ReadValue<float>() * Time.deltaTime;

    private void HandleMove()
    {
        Vector2 move = movementAction.ReadValue<Vector2>();

        Vector3 movementDirection = move.x * transform.right + move.y * transform.forward;

        rigidBody.linearVelocity = movementDirection * movementSpeed * Time.deltaTime;
    }
}
