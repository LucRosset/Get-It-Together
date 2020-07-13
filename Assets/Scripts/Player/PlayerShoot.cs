using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Cached references
    Transform body;

    [SerializeField]
    private GameObject _laserPrefab = null;
    [SerializeField]
    private int _laserDamage = 10;
    [SerializeField]
    private AudioClip _shot = null;

    void Start()
    {
        // Cache references
        body = transform.GetChild(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            //Quaternion playerBodyRotationNormal = transform.GetChild(0).transform.rotation*Quaternion.Euler(0,0,90f);
            //float facingAngle = transform.rotation.eulerAngles.z;
            PlayerLaser newLaser = Instantiate(
                _laserPrefab,
                body.position,
                body.rotation
            ).GetComponent<PlayerLaser>();
            newLaser.SetDamage(_laserDamage);
            AudioSource.PlayClipAtPoint(_shot, Camera.main.transform.position, .3f);
        }
    }
}
