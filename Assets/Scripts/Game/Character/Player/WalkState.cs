using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State 
{
    public AnimationClip anim;
    public AudioClip audi;
    public override void Enter()
    {
        animator.Play(anim.name);
        audioSource.clip = audi;
        audioSource.loop = true;
        audioSource.Play();

        isComplete = false;
    }

   

    public override void Do()
    {
        if  (input.movementInput == Vector2.zero)
            {
                isComplete = true;
            }
    }

    public override void Exit()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }

    public void SetAnimation(AnimationClip newAnim)
    {

        anim = newAnim;
    }
}
