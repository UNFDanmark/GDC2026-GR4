using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    
    public InputAction pauseAction;
    public GameObject pauseMenu;

    void Start()
    {
        instance = this;
        
        pauseAction.Enable();
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            togglePauseMenu();
        }
        
        if(pauseMenu.activeSelf && !SceneTransition.changingScene) Time.timeScale = 0;
    }

    public void togglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
        Cursor.lockState = pauseMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
