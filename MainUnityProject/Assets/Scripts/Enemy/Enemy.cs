using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 1;

    public string[] weakness = new string[3];

    public bool canMove = true;
    public bool killable = true;
    public GameObject[] collateralDamage;

    [Header("Sounds")]
    [SerializeField] public AudioSource sound;
    public AudioClip deathSound;
    public AudioClip walkingSound;
    public AudioClip attackSound;
    
    [Header("nix pille")]
    public bool dead = false;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] Transform player;

    [SerializeField] Animator animator;

    [SerializeField] bool targetProtected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (canMove) agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(walkingSoundLoop());
    }

    void Update()
    {
        if (!targetProtected && canMove)
        {
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(player.position, path) && path.status == NavMeshPathStatus.PathComplete){
                agent.speed = speed;
                agent.SetDestination(player.position);
            }
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
            StartCoroutine(deathWithSound());
        }
        else
        {
            Component c;
            if(TryGetComponent(typeof(Collider), out c)) ((Collider)c).enabled = false;
            foreach(Collider cc in transform.GetComponentsInChildren<Collider>()) cc.enabled = false;
            foreach (GameObject g in collateralDamage)
            {
                if(g.TryGetComponent(typeof(Collider), out c)) ((Collider)c).enabled = false;
                foreach(Collider cc in g.GetComponentsInChildren<Collider>()) cc.enabled = false;
            }
        }

    }

    IEnumerator deathWithSound()
    {
        canMove = false;
        Component c;
        if(TryGetComponent(typeof(MeshRenderer), out c)) ((MeshRenderer)c).enabled = false;
        foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>()) r.enabled = false;
        foreach (SkinnedMeshRenderer r in GetComponentsInChildren<SkinnedMeshRenderer>()) r.enabled = false;

        yield return new WaitForSeconds(deathSound.length);
        Destroy(gameObject);
    }

    IEnumerator walkingSoundLoop()
    {
        if(!canMove) yield break;
        while(!dead)
        {
            if (agent.velocity.magnitude > 0.5)
            {
                sound.PlayOneShot(walkingSound);
                yield return new WaitForSeconds(walkingSound.length);
            }
            else yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
