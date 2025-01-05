using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageAttack : MonoBehaviour
{
    public GameObject attackPoint;
    public float radius;
    public LayerMask player;
    public int damage;
    // Start is called before the first frame update
    private void Attack()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, player);
        foreach (Collider2D playerGameobject in players)
        {
            // Debug.Log("Hit creature");
            Health health = playerGameobject.GetComponent<Health>();
            health.TakeDamage(damage);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}
