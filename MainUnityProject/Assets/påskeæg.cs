using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class påskeæg : MonoBehaviour
{
    string[] taporder = new string[11];
    public InputAction anyKeyboard;

    void Start()
    {
        anyKeyboard.Enable();
    }

    void Update()
    {
        if (!anyKeyboard.WasPressedThisFrame()) return;
        
        
    }
}
