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
    
    [SerializeField] private float horizontalLock;

    private void OnEnable()
    {
        cam = Camera.main;
        yRotationAction.Enable();
        xRotationAction.Enable();
    }

    private void Update()
    {
        Quaternion rotation = Quaternion.identity;

        float toRotateY = yRotationAction.ReadValue<float>() * sensitivityY;
        float toRotateX = xRotationAction.ReadValue<float>() * sensitivityX;
        
        transform.parent.Rotate(transform.parent.up, toRotateX, Space.World);
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x += toRotateY;
        /*if (rot.x >= horizontalLock)
        {
            rot.x = horizontalLock;
        }
        else if (rot.x <= -horizontalLock)
        {
            rot.x = -horizontalLock;
        }*/
        transform.rotation = Quaternion.Euler(rot);
        //transform.Rotate(transform.parent.right, toRotateY, Space.World);
    }
}
