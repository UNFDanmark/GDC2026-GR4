using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class drawingController : MonoBehaviour
{
    public static drawingController instance;
    
    public InputAction drawToggleAction;
    public drawing drawer;

    public bool isDrawing = false;

    void Start()
    {
        instance = this;
        
        drawToggleAction.Enable();
        Disable();
    }

    void Disable()
    {
        drawer.Stop();
        foreach (RectTransform r in drawer.prikker)
        {
            r.gameObject.SetActive(false);
        }
        drawer.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isDrawing = false;
    }

    void Enable()
    {
        drawer.gameObject.SetActive(true);
        foreach (RectTransform r in drawer.prikker)
        {
            r.gameObject.SetActive(true);
        }
        isDrawing = true;
    }

    void Update()
    {
        if (drawToggleAction.IsPressed() && !isDrawing)
        {
            Enable();
        }
        else if (!drawToggleAction.IsPressed() && isDrawing)
        {
            Disable();
        }
    }
}
