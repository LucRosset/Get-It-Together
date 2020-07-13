using UnityEngine;

public class Shocker : MonoBehaviour
{
    [Tooltip("Ammount of damage taken for touching the shocker")]
    [SerializeField] int damage = 15;
    [SerializeField] AudioClip shock = null;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health) { health.TakeDamage(damage); }
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(shock, Camera.main.transform.position);
        }

    }
}
