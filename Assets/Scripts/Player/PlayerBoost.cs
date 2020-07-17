using System.Collections;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    [Tooltip("Boosting time")]
    [SerializeField] float boostTime = .1f;
    [Tooltip("Cooldown between a boost and the next warmup")]
    [SerializeField] float cooldownTime = 1f;

    // Cached references
    Animator fireAnimator;

    public bool idle { get; private set; } = true;
    public bool boosting { get; private set; } = false;

    void Start()
    {
        // Cache references
        fireAnimator = GameObject.Find("/Player/Body/Fire").GetComponent<Animator>();

        idle = true;
        boosting = false;
    }

    void Update()
    {
        if (idle && Input.GetButton("Boost"))
        {
            idle = false;
            StartCoroutine(StartBoost());
        }
    }

    IEnumerator StartBoost()
    {
        // Start propelling the ship
        boosting = true;
        fireAnimator.SetBool("boost", true);
        yield return new WaitForSeconds(boostTime);
        // Stop the ship
        boosting = false;
        fireAnimator.SetBool("boost", false);
        yield return new WaitForSeconds(cooldownTime);
        // Allow a new boost
        idle = true;
    }
}
