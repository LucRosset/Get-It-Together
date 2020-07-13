using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Cached references
    Slider slider;
    Health playerHealth;

    [Tooltip("Rate at which the health bar changes visually. Does no affect actual health")]
    [SerializeField] float rate = .07f;

    void Start()
    {
        // Cache references
        slider = GetComponent<Slider>();
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    void Update()
    {
        slider.value = Mathf.MoveTowards(
            slider.value,
            playerHealth.getHealthNorm(),
            rate * Time.deltaTime
        );
    }
}
