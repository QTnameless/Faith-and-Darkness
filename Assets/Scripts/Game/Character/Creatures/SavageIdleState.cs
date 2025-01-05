using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageIdleState : SavageState
{
    public AnimationClip anim;
    public override void Enter()
    {
        animator.Play(anim.name);
        isComplete = false;
    }
    

    public override void Do()
    {
        // Stay idle, no action is required here.
        isComplete = true; // Remains in idle unless changes.
    }

    public override void Exit() { }
}

