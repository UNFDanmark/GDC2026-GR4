using System.Collections;
using System.Collections.Generic;
using Radishmouse;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class drawing : MonoBehaviour
{

    [SerializeField] InputAction drawAction;
    [SerializeField] InputAction mousePositionAction;

    
    [Header("no touchies, this only for debugging!!!!")]
    [SerializeField] Transform prikParent;
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

        prikParent = GameObject.FindGameObjectWithTag("Prik Parent").transform;
        int i = 0;
        foreach (RectTransform t in prikParent.GetComponentsInChildren<RectTransform>())
        {
            if (t == prikParent) continue;
            prikker[i] = t;
            i++;
        }
    }
    
    void Update()
    {
        if (drawAction.IsPressed()) // && !isDrawing
        {
            trailRenderer.enabled = true;
            trailRenderer.time = int.MaxValue;
            Vector2 v = mousePositionAction.ReadValue<Vector2>();
            transform.position = cam.ScreenToWorldPoint(new Vector3(v.x, v.y, 1));

            foreach (RectTransform t in prikker)
            {
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, t.position);
                print(screenPos); //NEEDS FIXING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                if (v.x >= screenPos.x - 20 && v.x <= screenPos.x + 20 &&
                    v.y >= screenPos.y - 20 && v.y <= screenPos.y + 20)
                {
                    t.gameObject.GetComponent<Image>().color = Color.cyan;
                }
            }
        }
        else
        {
            trailRenderer.Clear();
            trailRenderer.enabled = false;
        }
        
    }
}
