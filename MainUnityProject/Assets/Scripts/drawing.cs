using System.Collections.Generic;
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
    public RectTransform[] prikker = new RectTransform[8];

    public int selectedSpot = -1;
    [SerializeField] Transform spotParent;
    public RectTransform[] spots = new RectTransform[3];
    public Image[] spotImages = new Image[3];

    private TrailRenderer trailRenderer;
    Camera cam;
    
    void OnEnable()
    {
        if(selectedSpot == -1) selectedSpot = 0;
        
        Cursor.lockState = CursorLockMode.None;
        
        drawAction.Enable();
        mousePositionAction.Enable();

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = int.MaxValue;
        
        cam = Camera.main;

        prikParent = GameObject.FindGameObjectWithTag("Prik Parent").transform;
        spotParent =  GameObject.FindGameObjectWithTag("Spot Parent").transform;
        int i = 0;
        foreach (RectTransform t in prikParent.GetComponentsInChildren<RectTransform>())
        {
            if (t == prikParent) continue;
            prikker[i] = t;
            t.name = i.ToString();
            i++;
        }
        i = 0;
        foreach (RectTransform t in spotParent.GetComponentsInChildren<RectTransform>())
        {
            if (t == spotParent) continue;
            spots[i] = t;
            spotImages[i] = t.GetComponent<Image>();
            t.name = i.ToString();
            i++;
        }
    }

    public void Stop()
    {
        if (!drawingController.instance.isDrawing) return;
        
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        
        foreach(RectTransform t in prikParent) t.gameObject.GetComponent<Image>().color = Color.white;

        string pattern = PatternManager.instance.matchPattern(order);
        if (pattern != null)
        {
            if (selectedSpot != -1)
            {
                drawingController.instance.queue[selectedSpot] = pattern;
                spotImages[selectedSpot].sprite = PatternManager.instance.findPattern(pattern).patternSprite;
                selectedSpot = (selectedSpot + 1)%3;
                OnEnable();
            }
            print(pattern);
        }
        
        order.Clear();
    }
    
    void Update()
    {
        if (selectedSpot > -1)
        {
            spots[selectedSpot].sizeDelta = new Vector2(200, 200);
            spots[(selectedSpot + 2)%3].sizeDelta = new Vector2(150, 150);
        }
        
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

            if (selectedSpot > -1)
            {
                spots[selectedSpot].sizeDelta = new Vector2(200, 200);
                if(selectedSpot > 0) spots[selectedSpot-1].sizeDelta = new Vector2(150, 150);
            }
        }
        else
        {
            Stop();
        }
        
    }
}
