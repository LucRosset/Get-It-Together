using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    // Cached references
    Rigidbody2D myRigidbody;
    GameObject player = null;

    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private int _damage = 10;

    [SerializeField]
    private AudioClip _hitSound = null;
    
    void Start()
    {
        // Cache references
        myRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("/Player");

        // Set direction
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set velocity
        direction.Normalize();
        myRigidbody.velocity = direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Deal damage
        Health health = other.gameObject.GetComponent<Health>();
        if (health) { health.TakeDamage(_damage); }
        // Play sound
        AudioSource.PlayClipAtPoint(_hitSound, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
