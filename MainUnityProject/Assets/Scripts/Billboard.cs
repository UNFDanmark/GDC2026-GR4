using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera c;

    void Start()
    {
        c = Camera.main;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - c.transform.position);
        transform.Rotate(new Vector3(0, 90, 0));
    }
}
