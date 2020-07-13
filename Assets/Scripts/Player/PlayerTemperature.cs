using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerTemperature : MonoBehaviour
{
    // Cached references
    Health health;
    SpriteRenderer myRenderer;
    AudioSource audioSource;

    Coroutine takeDamageRoutine;

    [Tooltip("When active, will not take damage from cold regions")]
    [SerializeField] bool active = false;
    [Tooltip("Sprite for inactive temperature control")]
    [SerializeField] Sprite inactiveCockpit = null;
    [Tooltip("Sprite for active temperature control")]
    [SerializeField] Sprite activeCockpit = null;
    [Space]
    [Tooltip("Period for taking cold damage, in seconds")]
    [SerializeField] float damagePeriod = 1f;
    [Tooltip("Ammount of damage taken by the cold, per period")]
    [SerializeField] int coldDamage = 7;

    void Start()
    {
        string prefix = "/Player/Body/";
        // Cache references
        health = GetComponent<Health>();
        myRenderer = GameObject.Find(prefix+"Cockpit").GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        active = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.gameObject.tag == "Cold")
        {
            takeDamageRoutine = StartCoroutine(TakeColdDamage());
            audioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cold" && takeDamageRoutine != null)
        {
            StopCoroutine(takeDamageRoutine);
            audioSource.Stop();
        }
    }

    IEnumerator TakeColdDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(damagePeriod);
            health.TakeDamage(coldDamage);
        }
    }

    public void SetTempControlActive(bool state)
    {
        active = state;
        if (state) { myRenderer.sprite = activeCockpit; }
        else { myRenderer.sprite = inactiveCockpit; }
    }
    public bool GetTempControlActive() { return active; }
}
