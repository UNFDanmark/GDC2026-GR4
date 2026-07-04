using System.Collections;
using System.Collections.Generic;
using Radishmouse;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class drawing : MonoBehaviour
{
    [Header("Settings")]
    [Range(10,1000)] [Tooltip("Lower = better but also chrome-level memory sucking")] public float drawingQuality = 1000;
    
    private int[] order = new int[8];
    [SerializeField] List<Vector2> drawPoints;

    UILineRenderer line;
    
    [SerializeField] bool isDrawing = false;

    [SerializeField] InputAction drawAction;
    [SerializeField] InputAction mousePositionAction;
    
    void Start()
    {
        drawAction.Enable();
        mousePositionAction.Enable();
        line =  GetComponent<UILineRenderer>();
        Restart();
    }

    void Restart()
    {
        Cursor.lockState = CursorLockMode.None;
        order = new int[8]{0,0,0,0,0,0,0,0};
        drawPoints = new List<Vector2>();
        isDrawing = false;
    }

    IEnumerator startRecording()
    {
        if (!drawAction.IsPressed())
        {
            Restart();
            yield return null;
        }
        else {
            isDrawing = true;
            drawPoints.Add(mousePositionAction.ReadValue<Vector2>());
            line.points = drawPoints.ToArray();
            yield return new WaitForSeconds(drawingQuality/1000);
            StartCoroutine(startRecording());
        }
    }
    
    void Update()
    {
        if (drawAction.IsPressed() && !isDrawing)
        {
            StartCoroutine(startRecording());
        }
        
    }
}
