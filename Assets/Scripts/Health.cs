using UnityEngine;

[RequireComponent(typeof(DestroySequence))]
public class Health : MonoBehaviour
{
    // Cached references
    DestroySequence destroyedObject;

    [Tooltip("maximum ammount of health/hitpoints the entity has")]
    [SerializeField] int maxHealth = 100;

    int health;

    void Start()
    {
        destroyedObject = GetComponent<DestroySequence>();
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            destroyedObject.Destroyed();
        }
    }

    /// <summary>
    /// Returns current normalized health (0 to 1, inclusive)
    /// </summary>
    public float getHealthNorm()
    {
        return (float)health/maxHealth;
    }

    public void Heal()
    {
        health = maxHealth;
    }
}
