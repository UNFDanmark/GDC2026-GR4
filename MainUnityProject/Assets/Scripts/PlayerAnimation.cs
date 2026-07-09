using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    public AudioSource audioSource;
    public float animationSpeed = 1;
    
    [Header("Fisting")]
    public Animator leftHandAnimator;
    public AnimationClip hitAnimation;
    public Animator rightHandAnimator;
    public AudioClip[] fistingSounds = new AudioClip[3];
    
    [Header("Legs")]
    public Animator leftLegAnimator;
    public AnimationClip kickAnimation;
    public Animator rightLegAnimator;
    public AudioClip[] kickSounds = new AudioClip[3];
    
    void Start()
    {
        instance = this;
    }
    
    public IEnumerator HandlePlayerAnimation(string[] queue, Enemy enemy)
    {
        int punches = 0;
        int kicks = 0;

        int i = 0;
        foreach (string action in queue)
        {
            if (action == "Punch")
            {
                if (punches%2 == 1) hitLeftHand();
                else hitRightHand();
                audioSource.PlayOneShot(fistingSounds[punches]);
                punches++;
                yield return new WaitForSeconds(hitAnimation.length / ((punches%2 == 1) ? rightHandAnimator.speed : leftHandAnimator.speed));
            }
            else if (action == "Kick")
            {
                if (kicks%2 == 1) kickLeftLeg();
                else kickRightLeg();
                audioSource.PlayOneShot(kickSounds[kicks]);
                kicks++;
                if (i < 2) yield return new WaitForSeconds(kickAnimation.length / ((kicks%2 == 1) ? rightLegAnimator.speed : leftLegAnimator.speed));
            }

            i++;

        }

        enemy.damage(queue);
    }
    
    public void hitRightHand()
    {
        rightHandAnimator.speed = animationSpeed;
        rightHandAnimator.SetTrigger("punch");
        print("right hand animation");
    }
    public void hitLeftHand()
    {
        leftHandAnimator.speed = animationSpeed;
        leftHandAnimator.SetTrigger("punch");
        print("left hand animation");
    }
    
    public void kickRightLeg()
    {
        rightLegAnimator.speed = animationSpeed;
        rightLegAnimator.SetTrigger("kick");
        print("right leg animation");
    }
    public void kickLeftLeg()
    {
        leftLegAnimator.speed = animationSpeed;
        leftLegAnimator.SetTrigger("kick");
        print("left leg animation");
    }
}
