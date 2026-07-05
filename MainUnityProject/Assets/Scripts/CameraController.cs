using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    Camera cam;
    
    //[SerializeField] private float yRotationSpeed;
    [SerializeField] private InputAction yRotationAction;
    [SerializeField] private InputAction xRotationAction;
    
    [SerializeField] float sensitivityX;
    [SerializeField] float sensitivityY;
    Vector3 lastScreenOrientiation = Vector3.forward;
    
    //[SerializeField] private int fov;
    [SerializeField] private float horizontalLock;
    //[SerializeField] GameObject lookAtTarget;

    private void OnEnable()
    {
        //yRotationAction.Enable();
        cam = Camera.main;
        yRotationAction.Enable();
        xRotationAction.Enable();
    }

    private void Update()
    {
        /*Quaternion rotationY = Quaternion.AngleAxis(yRotationSpeed * yRotationAction.ReadValue<float>() * Time.deltaTime, transform.right);
        Vector3 currentLookAtPosition = lookAtTarget.transform.position;
        Vector3 newLookAtPosition = rotationY * currentLookAtPosition;
        lookAtTarget.transform.position = newLookAtPosition;*/
        //cam.fieldOfView = fov;
        
        Quaternion rotation = Quaternion.identity;

        float toRotateY = yRotationAction.ReadValue<float>() * sensitivityY;
        float toRotateX = xRotationAction.ReadValue<float>() * sensitivityX;
        
        toRotateX = Mathf.Clamp(cam.transform.rotation.eulerAngles.x + toRotateX, -horizontalLock, horizontalLock);
        Quaternion rotationX = Quaternion.AngleAxis(toRotateX, transform.up);
        Quaternion rotationY = Quaternion.AngleAxis(toRotateY, transform.right);
        lastScreenOrientiation = rotationX * (rotationY * lastScreenOrientiation);
        cam.transform.LookAt(lastScreenOrientiation);
    }
}
