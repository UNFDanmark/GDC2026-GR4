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
        if (MenuManager.instance.pauseMenu.activeSelf) return;
        HandleMove();
    }
    
    private void HandleMove()
    {
        Vector2 move = movementAction.ReadValue<Vector2>();
        Vector3 movementDirection = move.x * transform.right + move.y * transform.forward;
        if (drawingController.instance.isDrawing)
        {
            move = Vector2.zero;
            movementDirection = Vector3.zero;
        }

        rigidBody.linearVelocity = movementDirection * (movementSpeed * Time.deltaTime) + new Vector3(0, rigidBody.linearVelocity.y, 0);
    }
}
