using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] AudioClip hyperspaceSound = null;

    // Cached references
    GameLoader loader;

    void Start()
    {
        // Cache references
        loader = FindObjectOfType<GameLoader>();
    }

    public void Win()
    {
        GameObject player = transform.parent.gameObject;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<PlayerBoost>().enabled = false;
    }

    public void PlayHyperspaceSound()
    {
        if (hyperspaceSound)
        {
            AudioSource.PlayClipAtPoint(hyperspaceSound, Camera.main.transform.position);
        }
    }

    public void Fade()
    {
        loader.LoadMenu();
    }
}
