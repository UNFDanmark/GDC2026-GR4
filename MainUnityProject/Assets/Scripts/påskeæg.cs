using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class påskeæg : MonoBehaviour
{
    public Material gaybama;
    
    [SerializeField] string[] taporder = new string[10];
    int i = 0;
    public InputAction up;
    public InputAction down;
    public InputAction left;
    public InputAction right;
    public InputAction b;
    public InputAction a;
    public InputAction start;

    void Start()
    {
        up.Enable();
        down.Enable();
        left.Enable();
        right.Enable();
        b.Enable();
        a.Enable();
        start.Enable();
    }

    void Update()
    {

        if (up.WasPressedThisFrame())
        {
            taporder[i] = "up";
            i++;
        }
        else if (down.WasPressedThisFrame())
        {
            taporder[i] = "down";
            i++;
        }
        else if (left.WasPressedThisFrame())
        {
            taporder[i] = "left";
            i++;
        }
        else if (right.WasPressedThisFrame())
        {
            taporder[i] = "right";
            i++;
        }
        else if (b.WasPressedThisFrame())
        {
            taporder[i] = "b";
            i++;
        }
        else if (a.WasPressedThisFrame())
        {
            taporder[i] = "a";
            i++;
        }

        if (i >= 10) i = 0;

        if (start.WasPressedThisFrame())
        {
            if (taporder.ToCommaSeparatedString() == "up, up, down, down, left, right, left, right, b, a")
            {
                doEasterEgg();
            }

            taporder = new String[10];
            i = 0;
        }
    }

    public void doEasterEgg()
    {
        print("gayify");
        foreach(MeshRenderer g in SceneManager.GetActiveScene().GetRootGameObjects()[0].GetComponentsInChildren<MeshRenderer>())
        {
            print(g.name);
            g.material = gaybama;
        }
    }
}
