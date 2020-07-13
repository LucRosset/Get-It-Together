using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Comet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private AudioClip _playerHit = null;

    // Cached references
    private Transform playerTransform;

    void Start()
    {
        // Cache references
        GameObject player = GameObject.Find("/Player");
        if (!player)
        {
            Destroy(gameObject);
            return;
        }
        playerTransform = player.transform;
    }

    void FixedUpdate() 
    {
        if(!playerTransform) return;

        transform.position = Vector2.MoveTowards(
            transform.position, playerTransform.position,
            _speed * Time.deltaTime
        );

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            AudioSource.PlayClipAtPoint(_playerHit, Camera.main.transform.position);
            ModulePanel modulePanel = other.gameObject.GetComponent<ModulePanel>();
            modulePanel.DowngradeToBase();
            Destroy(gameObject);
        }
    }
}
