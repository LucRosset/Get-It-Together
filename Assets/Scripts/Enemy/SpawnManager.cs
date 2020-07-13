using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour 
{
  [SerializeField]
  private GameObject _enemyPrefab = null;

  [SerializeField]
  private GameObject _enemyContainer = null;

  [Tooltip("Maximum number of enemies of this type to be spawned")]
  [SerializeField]
  private int _maxEnemiesToSpawn = 5;

  public int numberOfSpawnedEnemies{ get; set; } = 0;

  void Start() 
  {
    StartCoroutine(SpawnRoutine());
  }
  
  void Update()
  {

  }

  IEnumerator SpawnRoutine()
  {
    while (true)
    {
      if(numberOfSpawnedEnemies < _maxEnemiesToSpawn){
        Vector2 positionToSpawn = new Vector2(Random.Range(-8f, 8f), 7);
        GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        numberOfSpawnedEnemies += 1;
      }
      yield return new WaitForSeconds(5.0f);
    }
  }


}