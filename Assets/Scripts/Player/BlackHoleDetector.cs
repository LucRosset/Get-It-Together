using System.Collections;
using UnityEngine;

public class BlackHoleDetector : MonoBehaviour
{
    // Cached references
    Health health;
    AudioSource audioSource;

    Coroutine takeDamageRoutine;
    
    [Tooltip("Period for taking damage, in seconds")]
    [SerializeField] float damagePeriod = 1f;
    [Tooltip("Ammount of damage taken by the black hole, per period")]
    [SerializeField] int blackHoleDamage = 4;

    void Start()
    {
        // Cache references
        health = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Black Hole")
        {
            takeDamageRoutine = StartCoroutine(TakeColdDamage());
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Black Hole" && takeDamageRoutine != null)
        {
            StopCoroutine(takeDamageRoutine);
            audioSource.Stop();
        }
    }

    IEnumerator TakeColdDamage()
    {
        while (true)
        {
            health.TakeDamage(blackHoleDamage);
            yield return new WaitForSeconds(damagePeriod);
        }
    }
}
