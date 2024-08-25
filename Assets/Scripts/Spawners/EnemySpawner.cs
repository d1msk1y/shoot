using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    private GameObject[] currentEnemies;

    private float distance;
    private float distanceUsed;
    public float velocityModifier;
    public static EnemySpawner instance;
    private float distToGo;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (distance < transform.position.x + 20)
            distance = transform.position.x + 20;

        distToGo = Mathf.Floor(distance - distanceUsed);

        if(distanceUsed < distance && distToGo > 0.05)
        {
            distanceUsed = distance;
            print(distToGo);
            SpawnEnemy();

        }
    }

    public void ResetEnemySpawner()
    {        
        distToGo = 0;
        distanceUsed = 0;
        distance = 0;

        foreach(GameObject enemies in currentEnemies)
        {
            Destroy(enemies);
        }

    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = SelectEnemyToSpawn();

        float yPos = Mathf.Floor(Mathf.Abs(Random.Range(0f, 1f) - Random.Range(0f, 1f)) * (1 + 300 - 100) + (-1));
        Vector2 posToSpawnEnemy = new Vector2(distance, yPos);

        Instantiate(enemyToSpawn, posToSpawnEnemy, Quaternion.identity);
    }

    private GameObject SelectEnemyToSpawn()
    {
        int index = Random.Range(0, enemies.Length);

        return enemies[index];

    }
}
