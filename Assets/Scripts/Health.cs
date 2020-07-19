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

    public void TakeDamage(int damage, AudioClip hitSound = null)
    {
        health -= damage;
        if (health <= 0) { destroyedObject.Destroyed(); }
        else if (hitSound) { AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position); }
    }

    /// <summary>
    /// Returns current normalized health (0 to 1, inclusive)
    /// </summary>
    public float getHealthNorm()
    {
        return (float)health/maxHealth;
    }

    /// <summary>Adds hitpoints to current health, up to maxHealth</summary>
    /// <param="restored">Ammount of health restored. If negative, restores all health</param>
    public void Heal(int restored = -1)
    {
        if (restored < 0) { health = maxHealth; }
        else { health = Mathf.Clamp(health+restored, 0, maxHealth); }
    }
}
