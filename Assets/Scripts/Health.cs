using UnityEngine;

[RequireComponent(typeof(Ship))]
public class Health : MonoBehaviour
{
    // Cached references
    Ship ship;

    [Tooltip("maximum ammount of health/hitpoints the entity has")]
    [SerializeField] int maxHealth = 100;

    int health;

    void Start()
    {
        ship = GetComponent<Ship>();
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ship.Destroyed();
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
