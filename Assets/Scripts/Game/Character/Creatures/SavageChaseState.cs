using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageChaseState : SavageState
{
    public AnimationClip anim;
    private Transform player;
    public float chaseSpeed;
    public float rotationSpeed = 5f; // Adjust rotation speed for smooth turning

  
    public void Setup(Rigidbody2D _body, Animator _animator, Transform _player)
    {
        base.Setup(_body, _animator);
        player = _player;
    }
    public override void Enter()
    {
        animator.Play(anim.name);
        isComplete = false;
    }


    public override void Do()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)body.position).normalized;

        // Calculate the angle in degrees the enemy needs to rotate to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Offset by 90 degrees to align with sprite's orientation

        // Smoothly interpolate the enemy's rotation to face the player
        body.rotation = Mathf.LerpAngle(body.rotation, angle, rotationSpeed * Time.deltaTime);

        // Move towards the player once facing them
        body.velocity = direction * chaseSpeed;


        isComplete = true;
    }

    public override void Exit()
    {
        body.velocity = Vector2.zero; // Stop movement on exit.
    }

}
