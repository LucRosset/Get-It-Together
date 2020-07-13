using UnityEngine;

/// <summary>
/// Override method Destroyed() to set a behaviour for when the ship reaches 0 health
/// </summary>
public class Ship : MonoBehaviour
{
    [SerializeField] AudioClip destroyed = null;
    [SerializeField] GameObject deathFX = null;
    [SerializeField] float sfxVolume = 1f;

    /// <summary>
    /// Override this to set a behaviour just before destroying the game object
    /// </summary>
    virtual public void Destroyed()
    {
        AudioSource.PlayClipAtPoint(destroyed, Camera.main.transform.position, sfxVolume);
        if (deathFX)
        {
            GameObject fx = Instantiate(
                deathFX,
                transform.position,
                Quaternion.identity
            );
            Destroy(fx, 2f);
        }
        Destroy(gameObject);
    }
}
