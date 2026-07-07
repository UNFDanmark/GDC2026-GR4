using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 1;

    public string[] weakness = new string[3];
    
    [Header("nix pille")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;

    [SerializeField] Animator animator;

    [SerializeField] bool targetProtected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!targetProtected)
        {
            agent.speed = speed;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.speed = 0;
        }
        
        animator.SetFloat("speed", agent.velocity.magnitude);
    }
}
