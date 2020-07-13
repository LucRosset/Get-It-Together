using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Upgrade powerup = null;

    [SerializeField]
    private bool hasUpgrade = false;

    [SerializeField]
    private Modules module = Modules.boost;

    [SerializeField]
    private AudioClip smashSound = null;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player"){
            AudioSource.PlayClipAtPoint(smashSound, Camera.main.transform.position);
            if(hasUpgrade){
                powerup.setModule(module);
                GameObject powerupInstance = Instantiate(powerup.gameObject, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
