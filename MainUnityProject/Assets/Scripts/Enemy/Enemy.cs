using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;

    [SerializeField] bool targetProtected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
    }
}
