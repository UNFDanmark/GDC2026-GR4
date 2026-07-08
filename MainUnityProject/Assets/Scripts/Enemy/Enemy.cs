using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 1;

    public string[] weakness = new string[3];

    public bool canMove = true;
    public bool killable = true;

    [Header("Sounds")]
    [SerializeField] AudioSource sound;
    public AudioClip deathSound;
    public AudioClip walkingSound;
    public AudioClip attackSound;
    
    [Header("nix pille")]
    [SerializeField] bool dead = false;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;

    [SerializeField] Animator animator;

    [SerializeField] bool targetProtected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (canMove) agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!targetProtected && canMove)
        {
            agent.speed = speed;
            agent.SetDestination(player.position);
        }
        else if (canMove)
        {
            agent.speed = 0;
        }
        
        if(canMove) animator.SetFloat("speed", agent.velocity.magnitude);
    }

    public bool damage(string[] combo)
    {
        if (combo.ToCommaSeparatedString().Equals(weakness.ToCommaSeparatedString()))
        {
            print("kill triggered");
            animator.SetTrigger("kill");
            return true;
        }
        else
        {
            print("resisted");
        }

        return false;
    }

    public void okKillMeNow()
    {
        if (dead) return;
        dead = true;
        print("AHHHHH");
        sound.PlayOneShot(deathSound);
        if (killable)
        {
            Destroy(gameObject);
        }
        else
        {
            Component c;
            if(TryGetComponent(typeof(Collider), out c)) ((Collider)c).enabled = false;
            foreach(Collider cc in transform.GetComponentsInChildren<Collider>()) cc.enabled = false;
        }

    }
}
