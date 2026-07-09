using System;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    [Header("General Options")]
    public float viewDistance;

    public int attackDamage = 20;
    public float attackCooldown = 1;
    private float attackCooldownMax;
    
    [Header("References")]
    [SerializeField] protected GameObject p;
    
    [SerializeField] protected Transform eyes = null;
    [SerializeField] protected Transform target;

    public Enemy enemy;

    [Header("Debugging Stuff")]
    [SerializeField] protected bool targetVisible = false;

    protected void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player");
        target = p.transform;

        eyes = null;
        for(int i = 0; i < transform.childCount; i++) if (transform.GetChild(i).name == "Eyes") eyes = transform.GetChild(i);

        enemy = GetComponent<Enemy>();
        attackCooldownMax = attackCooldown;
    }

    protected void Update()
    {
        RaycastHit hit;
        Physics.Raycast(eyes.transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance);
        targetVisible = (hit.transform == target);
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) attackCooldown = 0;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = targetVisible ? Color.red :  Color.blue;
        Gizmos.DrawLine(eyes.transform.position, eyes.transform.position + transform.forward * viewDistance);
    }

    protected bool OnCooldown()
    {
        return attackCooldown > 0;
    }

    protected void SetCooldown()
    {
        attackCooldown = attackCooldownMax;
    }

    public abstract void Attack();
}
