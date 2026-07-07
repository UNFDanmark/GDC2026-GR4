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
    
    [SerializeField] private float horizontalDegreesOfFreedom;

    [SerializeField] Rigidbody rb;
    
    private void OnEnable()
    {
        cam = Camera.main;
        yRotationAction.Enable();
        xRotationAction.Enable();
        
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (drawingController.instance.isDrawing || MenuManager.instance.pauseMenu.activeSelf) return;
        Quaternion rotation = Quaternion.identity;

        float toRotateY = yRotationAction.ReadValue<float>() * sensitivityY;
        float toRotateX = xRotationAction.ReadValue<float>() * sensitivityX;
        
        transform.parent.Rotate(transform.parent.up, toRotateX, Space.World);
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x += toRotateY;
        if (rot.x > 180) rot.x = Mathf.Clamp(rot.x, (360 - (horizontalDegreesOfFreedom / 2)), (360 + (horizontalDegreesOfFreedom / 2)));
        else rot.x = Mathf.Clamp(rot.x, -horizontalDegreesOfFreedom/2, horizontalDegreesOfFreedom/2);
        transform.rotation = Quaternion.Euler(rot);
    }
}
