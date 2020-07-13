using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;

    [Tooltip("Min distance from player")]
    [SerializeField]
    private float _minDistanceToPlayer = 6.0f;

    [SerializeField]
    private GameObject _laserPrefab = null;

    [SerializeField]
    private float _fireRate = 3.0f;

    private bool active = false;
    Coroutine shootingCoroutine;

    public GameObject player;
    
    void Start()
    {
        player = GameObject.Find("/Player");
    }
    
    void Update()
    {
        if(!player) return;

        if (active)
        {
            float distanceToPlayer = 0.0f;
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > _minDistanceToPlayer)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position, player.transform.position,
                    _speed * Time.deltaTime
                );
            }
        }
    }

    IEnumerator ShootPlayerRoutine(){
        while(true){
            if(!player) break;
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_fireRate);
        }
    }

    void OnDestroy() {
        SpawnManager enemySpawnManager = GameObject.Find("SpawnManager")?.GetComponent<SpawnManager>();
        if(!enemySpawnManager) return;
        enemySpawnManager.numberOfSpawnedEnemies -= 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            active = true;
            shootingCoroutine = StartCoroutine(ShootPlayerRoutine());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            active = false;
            StopCoroutine(shootingCoroutine);
        }
    }

}
