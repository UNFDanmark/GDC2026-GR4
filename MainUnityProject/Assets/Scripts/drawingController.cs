using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class drawingController : MonoBehaviour
{
    public static drawingController instance;
    
    public InputAction drawToggleAction;
    public drawing drawer;

    public bool isDrawing = false;

    [SerializeField] bool slowing = false;
    [SerializeField] bool speeding = false;

    void Start()
    {
        instance = this;
        
        drawToggleAction.Enable();
        Disable();
    }

    void Disable()
    {
        StartCoroutine(StopBulletTime());
        drawer.Stop();
        foreach (RectTransform r in drawer.prikker)
        {
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
