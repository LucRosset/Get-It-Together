using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioClip startSound = null;

    void Update()
    {
        if (Input.GetButtonDown("Laser"))
        {
            AudioSource.PlayClipAtPoint(startSound, Camera.main.transform.position);
            FindObjectOfType<GameLoader>().Startgame();
        }
    }
}
