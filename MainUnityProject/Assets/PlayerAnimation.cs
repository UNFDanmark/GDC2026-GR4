using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;
    
    public Animator leftHandAnimator;
    public AnimationClip hitAnimation;
    public Animator rightHandAnimator;
    
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
                //do kicks so cool
                kicks++;
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
}
