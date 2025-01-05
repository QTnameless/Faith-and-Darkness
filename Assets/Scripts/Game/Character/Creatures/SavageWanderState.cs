using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageWanderState : SavageState
{
    public AnimationClip anim;
    public float wanderSpeed = 2f;
    public float wanderDuration = 3f; // Time spent wandering in one direction
    private float endTime;
    private Vector2 wanderDirection;

    public override void Enter()
    {
        // Play wandering animation
        if (anim != null) animator.Play(anim.name);

        // Reset wander state
        isComplete = false;
        endTime = Time.time + wanderDuration;

        // Choose a random direction to move
        wanderDirection = Random.insideUnitCircle.normalized;
    }

    public override void Do()
    {
        if (Time.time < endTime)
        {
            // Move in the chosen direction
            body.velocity = wanderDirection * wanderSpeed;
        }
        else
        {
            // Stop wandering and mark as complete
            body.velocity = Vector2.zero;
            isComplete = true;
        }
    }

    public override void Exit()
    {
        // Stop movement when exiting the state
        body.velocity = Vector2.zero;
    }
}