using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBall : MonoBehaviour
{
    GameObject gunPowerUp = null;
    // Start is called before the first frame update
    void Start()
    {
        gunPowerUp = GameObject.Find("/Upgrades/Gun");
        gunPowerUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Black Hole")
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
            Destroy(gameObject);
            gunPowerUp.SetActive(true);
        }
    }

}
