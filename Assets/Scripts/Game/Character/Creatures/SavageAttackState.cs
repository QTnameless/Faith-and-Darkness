using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageAttackState : SavageState
{
    public AnimationClip anim;        // The attack animation clip
    public float cooldown = 2f;          // Cooldown time before another attack
    public override void Enter()
    {
        animator.Play(anim.name);    // Start the attack animation


        isComplete = false;             // Reset completion flag
        startTime = Time.time;
       
            // Record the start time of the attack
    }

    public override void Do()
    {
        if ( time >= anim.length) // Check if the attack animation has finished
        {
            // Attack completed
            isComplete = true;   //
        }
    }

    public override void Exit()
    {
       // Reset attack flag on exit
        // Any exit logic for attack state (e.g., stop animation or reset flags)
    }
}
