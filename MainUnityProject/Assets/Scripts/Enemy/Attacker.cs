using System;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [Header("General Options")]
    public float viewDistance;
    
    [Header("References")]
    [SerializeField] GameObject p;
    
    [SerializeField] Transform eyes = null;
    [SerializeField] Transform target;

    [Header("Debugging Stuff")]
    [SerializeField] bool targetVisible = false;

    protected void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
        target = p.transform;

        eyes = null;
        for(int i = 0; i < transform.childCount; i++) if (transform.GetChild(i).name == "Eyes") eyes = transform.GetChild(i);
    }

    protected void Update()
    {
        RaycastHit hit;
        Physics.Raycast(eyes.transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance);
        if(hit.transform == target) targetVisible = true;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = targetVisible ? Color.red :  Color.limeGreen;
        Gizmos.DrawLine(eyes.transform.position, eyes.transform.position + transform.forward * viewDistance);
    }

    public abstract void Attack();
}
