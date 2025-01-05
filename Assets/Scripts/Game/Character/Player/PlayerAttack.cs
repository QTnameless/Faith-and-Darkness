using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAttack : MonoBehaviour
{
    

    public GameObject attackPoint;
    public float radius ;
    public LayerMask creatures;
    public int damage;

 
    private void Attack()
    {
        Collider2D[] creature = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, creatures);
        foreach (Collider2D creatureGameobject in creature)
        {
            // Debug.Log("Hit creature");
            Health health = creatureGameobject.GetComponent<Health>();
                health.TakeDamage(damage);

        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}