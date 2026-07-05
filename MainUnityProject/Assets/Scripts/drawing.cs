using System.Collections;
using System.Collections.Generic;
using Radishmouse;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class drawing : MonoBehaviour
{

    [SerializeField] InputAction drawAction;
    [SerializeField] InputAction mousePositionAction;
    
    [SerializeField] RectTransform[] prikker = new RectTransform[8];

    private TrailRenderer trailRenderer;
    Camera cam;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        
        drawAction.Enable();
        mousePositionAction.Enable();

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = int.MaxValue;
        
        cam = Camera.main;
    }
    
    void Update()
    {
        if (drawAction.IsPressed()) // && !isDrawing
        {
            trailRenderer.enabled = true;
            trailRenderer.time = int.MaxValue;
            Vector2 v = mousePositionAction.ReadValue<Vector2>();
            transform.position = cam.ScreenToWorldPoint(new Vector3(v.x, v.y, 1));
        }
        else
        {
            trailRenderer.Clear();
            trailRenderer.enabled = false;
        }
        
    }
}
