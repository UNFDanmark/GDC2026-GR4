using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Radishmouse;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class drawing : MonoBehaviour
{

    [SerializeField] InputAction drawAction;
    [SerializeField] InputAction mousePositionAction;


    [Header("no touchies, this only for debugging!!!!")] [SerializeField]
    List<RectTransform> order = new List<RectTransform>();
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
            t.name = i.ToString();
            i++;
        }
    }

    void Stop()
    {
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        
        foreach(RectTransform t in prikParent) t.gameObject.GetComponent<Image>().color = Color.white;

        string pattern = PatternManager.instance.matchPattern(order);
        if (pattern != null)
        {
            print(pattern);
        }
        
        order.Clear();
    }
    
    void Update()
    {
        if (drawAction.IsPressed()) // && !isDrawing
        {
            trailRenderer.enabled = true;
            trailRenderer.time = int.MaxValue;
            Vector2 v = mousePositionAction.ReadValue<Vector2>();
            transform.position = cam.ScreenToWorldPoint(new Vector3(v.x, v.y, 1));

            int i = 0;
            foreach (RectTransform t in prikker)
            {
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, t.position);
                bool hasBeenTouched = order.Contains(t);
                if (v.x >= screenPos.x - 20 && v.x <= screenPos.x + 20 &&
                    v.y >= screenPos.y - 20 && v.y <= screenPos.y + 20 &&
                    !hasBeenTouched)
                {
                    order.Add(t);
                    t.gameObject.GetComponent<Image>().color = Color.cyan;
                    trailRenderer.AddPosition(cam.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, 1)));
                }
                i++;
            }
        }
        else
        {
            Stop();
        }
        
    }
}
