using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageAI: MonoBehaviour
{
    public SavageMovement savageMovement;

    public float chaseRange = 10f;
   
    public float attackRange = 2f;
 
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        /*
        if (distanceToPlayer <= attackRange)
        {
            savageMovement.SetState(savageMovement.attackState);
        }*/
         if (distanceToPlayer <= chaseRange)
        {
            savageMovement.SetState(savageMovement.chaseState);
        }
        else
        {
            savageMovement.SetState(savageMovement.idleState);
        }
    }
}
