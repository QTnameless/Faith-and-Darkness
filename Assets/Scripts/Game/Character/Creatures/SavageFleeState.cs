using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageFleeState : SavageState
{
    public AnimationClip anim;        // Flee animation
    private Transform player;         // Reference to the player
    public float fleeSpeed;           // Speed at which the Savage flees
    public float rotationSpeed = 5f;  // Speed of smooth rotation

    public void Setup(Rigidbody2D _body, Animator _animator, Transform _player)
    {
        base.Setup(_body, _animator);
        player = _player;
    }

    public override void Enter()
    {
        if (anim != null)
        {
            animator.Play(anim.name); // Play flee animation
        }
        isComplete = false; // Reset completion flag
    }

    public override void Do()
    {
        

        // Calculate direction away from the player
        Vector2 direction = ((Vector2)body.position - (Vector2)player.position).normalized;

        // Calculate the angle in degrees for rotation
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Smoothly interpolate the rotation
        body.rotation = Mathf.LerpAngle(body.rotation, targetAngle, rotationSpeed * Time.deltaTime);

        // Move in the opposite direction of the player
        body.velocity = direction * fleeSpeed;

        isComplete = true; // Allow transitions once fleeing is complete
    }

    public override void Exit()
    {
        body.velocity = Vector2.zero; // Stop movement on exit
    }

}
