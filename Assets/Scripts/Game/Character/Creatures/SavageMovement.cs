using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageMovement : MonoBehaviour
{
    public SavageIdleState idleState;
    public SavageChaseState chaseState;
    public SavageAttackState attackState;
    public SavageDieState dieState;
    public SavageFleeState fleeState;
    // Dependencies
    public Rigidbody2D rigidBody;
    public Animator animator;

    // Current state and player reference
    public SavageState currentState;
    private Transform player;
    public Torch playerTorch;

    private float lastAttackTime;

    // Range settings
    
    public float chaseRange ;
    public float attackRange ;
    public float fleeRange ;
    
    void Start()
    {
        // Cache the player reference
        player = FindObjectOfType<PlayerMovement>().transform;

        // Setup states
        idleState.Setup(rigidBody, animator);
        chaseState.Setup(rigidBody, animator, player);
        fleeState.Setup(rigidBody, animator, player);
        attackState.Setup(rigidBody, animator);
        dieState.Setup(rigidBody, animator);

        // Set initial state
        currentState = idleState;
        currentState.Initialise();
        currentState.Enter();
    }

    void Update()
    {
        // Update the current state's behavior
        currentState.Do();

        // Only select a new state if the current state is marked complete
        if (currentState.isComplete)
        {
            SelectState();
        }
    }

    private void SelectState()
    {
        float sqrDistanceToPlayer = (player.position - transform.position).sqrMagnitude;

        // Check for FleeState first (highest priority)
        if (playerTorch.isOn && sqrDistanceToPlayer <= fleeRange * fleeRange)
        {
            SetState(fleeState);
            return; // Exit after transitioning to FleeState
        }

        // Check for AttackState next
        if (sqrDistanceToPlayer <= attackRange * attackRange && Time.time >= lastAttackTime + attackState.cooldown)
        {
            SetState(attackState);
            lastAttackTime = Time.time; // Reset cooldown
            return;
        }

        // Check for ChaseState
        if (sqrDistanceToPlayer <= chaseRange * chaseRange)
        {
            SetState(chaseState);
            return;
        }

        // Default to IdleState
        SetState(idleState);
    }

    public void SetState(SavageState newState)
    {
        if (currentState == dieState) return; // Once enter DieState , Stay in dieState forever
        if (currentState != newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Initialise();
            currentState.Enter();
        }
    }

}