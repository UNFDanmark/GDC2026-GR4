using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float yRotationSpeed;
    [SerializeField] private InputAction yRotationAction;
    [SerializeField] GameObject lookAtTarget;

    private void OnEnable()
    {
        yRotationAction.Enable();
    }

    private void Update()
    {
        Quaternion rotationY = Quaternion.AngleAxis(yRotationSpeed * yRotationAction.ReadValue<float>() * Time.deltaTime, transform.right);
        Vector3 currentLookAtPosition = lookAtTarget.transform.position;
        Vector3 newLookAtPosition = rotationY * currentLookAtPosition;
        lookAtTarget.transform.position = newLookAtPosition;
    }
}
