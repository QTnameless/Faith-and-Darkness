using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth; // Maximum health for this character
    public int currentHealth; // Current health value

    public UnityEvent OnDamage; // Event for when the character takes damage
    public UnityEvent OnDeath; // Event for when the character dies
    
    public bool IsDead { get; private set; } = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        //Debug.Log($"[Health] Initialized: Max Health = {maxHealth}, Current Health = {currentHealth}");
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //Debug.Log($"[Health] Took Damage: Amount = {amount}, Current Health = {currentHealth}/{maxHealth}");

        OnDamage?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /*
    public void Heal(float amount)
    {
        if (IsDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"[Health] Healed: Amount = {amount}, Current Health = {currentHealth}/{maxHealth}");
        OnHealthChanged?.Invoke();
    }
    */

    private void Die()
    {
        IsDead = true;
        //Debug.Log($"[Health] Character Died. Current Health = {currentHealth}");
        OnDeath?.Invoke();
    }

    public int GetHealth()
    {
      //  Debug.Log($"[Health] GetHealth: Current Health = {currentHealth}");
        return currentHealth;
    }

    public int GetMaxHealth()
    {
       // Debug.Log($"[Health] GetMaxHealth: Max Health = {maxHealth}");
        return maxHealth;
    }
    
    public float GetHealthProportion()
    {
        float healthProportion = (float)currentHealth / maxHealth ; // Correct calculation for percentage
       // Debug.Log($"[Health] GetHealthPercentage: Health Propotion = {healthProportion}");
        return healthProportion;
    }
}