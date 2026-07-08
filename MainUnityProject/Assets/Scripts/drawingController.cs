using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class drawingController : MonoBehaviour
{
    public static drawingController instance;
    
    public InputAction drawToggleAction;
    public InputAction attackAction;
    public float attackRange = 10f;
    public drawing drawer;

    public bool isDrawing = false;
    public Sprite emptySprite;

    public string[] queue = new string[3]{"","",""};
    
    [SerializeField] bool slowing = false;
    [SerializeField] bool speeding = false;
    Camera cam;

    void Start()
    {
        instance = this;
        
        drawToggleAction.Enable();
        attackAction.Enable();
        Disable();
        
        cam = Camera.main;
    }

    void Disable()
    {
        StartCoroutine(StopBulletTime());
        drawer.selectedSpot = -1;
        drawer.Stop();
        foreach (RectTransform r in drawer.prikker)
        {
            r.gameObject.SetActive(false);
        }
        foreach (RectTransform r in drawer.spotBackgrounds)
        {
            r.gameObject.SetActive(false);
        }
        foreach (RectTransform r in drawer.spots)
        {
            r.sizeDelta = new Vector2(125, 125);
            r.gameObject.SetActive(false);
        }
        drawer.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        isDrawing = false;
    }

    IEnumerator StartBulletTime()
    {
        slowing = true;
        speeding = false;
        while (Time.timeScale > 0.3f)
        {
            if (!slowing) 
            {
                Time.timeScale = 1;
                yield return null;
            }
            yield return new WaitForSeconds(Time.deltaTime);
            Time.timeScale -= Time.deltaTime;
        }
        Time.timeScale = 0.3f;
        slowing = false;
    }

    IEnumerator StopBulletTime()
    {
        speeding = true;
        slowing = false;
        while (Time.timeScale < 1f)
        {
            if (!speeding)
            {
                Time.timeScale = 0.3f;
                yield return null;
            }
            yield return new WaitForSeconds(Time.deltaTime);
            Time.timeScale += Time.deltaTime;
        }

        Time.timeScale = 1f;
        speeding = false;
    }

    void Enable()
    {
        StartCoroutine(StartBulletTime());
        drawer.gameObject.SetActive(true);
        foreach (RectTransform r in drawer.prikker)
        {
            r.gameObject.SetActive(true);
        }
        foreach (RectTransform r in drawer.spotBackgrounds)
        {
            r.gameObject.SetActive(true);
        }
        foreach (RectTransform r in drawer.spots)
        {
            r.gameObject.SetActive(true);
        }
        isDrawing = true;
    }

    void Update()
    {
        if (drawToggleAction.IsPressed() && !isDrawing && !MenuManager.instance.pauseMenu.activeSelf)
        {
            Enable();
        }
        else if (!drawToggleAction.IsPressed() && isDrawing)
        {
            Disable();
        }

        if (attackAction.WasPressedThisFrame())
        {
            if (!queue.ToArray().Contains(""))
            {
                RaycastHit hit;
                bool hitSomething = Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, attackRange);
                if (hitSomething && (hit.transform.gameObject.CompareTag("Enemy") || hit.transform.parent.gameObject.CompareTag("Enemy")))
                {
                    Transform enemyTransform = hit.transform;
                    Enemy enemy = enemyTransform.GetComponentInParent<Enemy>();

                    foreach (RectTransform r in drawer.spots)
                    {
                        r.GetComponent<Image>().sprite = emptySprite;
                    }

                    string[] temp = queue;
                    queue = new string[3];
                    
                    StartCoroutine(PlayerAnimation.instance.HandlePlayerAnimation(temp, enemy));
                }
                else
                {
                    print("FAILED: hit " + hit.transform.name);
                }
            }
            else
            {
                print("FAILED: queue has empty something");
            }
        }
    }
}
