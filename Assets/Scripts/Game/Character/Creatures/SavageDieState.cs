using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageDieState : SavageState
{

    public AnimationClip anim;

    public override void Enter()
    {
        animator.Play(anim.name);
        body.velocity = Vector2.zero; // Stop all movement
        body.isKinematic = true;
    }
    public override void Do()
    {
        // No further logic; remains in this state indefinitely
     

        // No further logic; remains in this state indefinitely
    }

    public override void Exit()
    {
        // No exit logic; this state is permanent
    }


}
