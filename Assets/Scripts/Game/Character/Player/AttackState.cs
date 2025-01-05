using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AnimationClip anim;
    public AudioClip audi;
   // private PlayerAttack playerAttack;

 
    public override void Enter()
    {
        animator.Play(anim.name);
       
        audioSource.clip = audi;
        audioSource.Play();

        input.EnableMovementAndRotation(false); // Prevent movement and rotation during attack


        isComplete = false;
    }

    public override void Do()
    {
        // Check if attack animation has completed
        if (time >= anim.length)
        {
            isComplete = true;
            // Transition to Idle or Walk based on movement input in PlayerMovement
           // playerAttack.playerMovement.SetState(playerAttack.playerMovement.idleState);
        }
    }

    public override void Exit()
    {
        audioSource.Stop();
        // playerAttack.ResetCooldown();

        input.EnableMovementAndRotation(true);

    }
}
