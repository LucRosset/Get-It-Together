using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private int _damage = 10;

    [SerializeField]
    private AudioClip _enemyHit = null;

    void Start()
    {
        // Set velocity
        float facingAngle = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
        Vector2 velocity = new Vector2(Mathf.Cos(facingAngle), Mathf.Sin(facingAngle));
        GetComponent<Rigidbody2D>().velocity = velocity * _speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health) { health.TakeDamage(_damage); }
        if (collision.gameObject.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(_enemyHit, transform.position);
        }
        Destroy(gameObject);
    }

    public void SetDamage(int dmg) { _damage = dmg; }
}
