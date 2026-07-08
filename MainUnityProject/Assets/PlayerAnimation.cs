using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    
    [Header("Fisting")]
    public Animator leftHandAnimator;
    public AnimationClip hitAnimation;
    public Animator rightHandAnimator;
    
    [Header("Legs")]
    public Animator leftLegAnimator;
    public AnimationClip kickAnimation;
    public Animator rightLegAnimator;
    
    void Start()
    {
        instance = this;
    }
    
    public IEnumerator HandlePlayerAnimation(string[] queue, Enemy enemy)
    {
        int punches = 0;
        int kicks = 0;
        
        foreach (string action in queue)
        {
            if (action == "Punch")
            {
                if (punches%2 == 1) hitLeftHand();
                else hitRightHand();
                punches++;
                yield return new WaitForSeconds(hitAnimation.length / ((punches%2 == 1) ? rightHandAnimator.speed : leftHandAnimator.speed));
            }
            else if (action == "Kick")
            {
                if (kicks%2 == 1) kickLeftLeg();
                else kickRightLeg();
                kicks++;
                yield return new WaitForSeconds(kickAnimation.length / ((kicks%2 == 1) ? rightLegAnimator.speed : leftLegAnimator.speed));
            }

        }

        enemy.damage(queue);
    }
    
    public void hitRightHand()
    {
        rightHandAnimator.SetTrigger("punch");
        print("right hand animation");
    }
    public void hitLeftHand()
    {
        leftHandAnimator.SetTrigger("punch");
        print("left hand animation");
    }
    
    public void kickRightLeg()
    {
        rightLegAnimator.SetTrigger("kick");
        print("right leg animation");
    }
    public void kickLeftLeg()
    {
        leftLegAnimator.SetTrigger("kick");
        print("left leg animation");
    }
}
