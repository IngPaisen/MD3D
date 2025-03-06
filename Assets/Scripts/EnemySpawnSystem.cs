using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy prefab to spawn
    public int maxEnemies = 10; // Mximum number of enemies allowed at the same time
    public float spawnCooldown = 2f; // Time delay between spawns
    public Transform[] spawnPoints; // Predefined spawn points

    private List<GameObject> enemyPool = new List<GameObject>(); // Pool of enemies for object pooling
    private int activeEnemies = 0; // Current number of active enemies
    private bool canSpawn = false; // Flag to control spawning

    // Start is called before the first frame update
    void Start()
    {
        InitializePool(); // Initialize enemy pool at the start
        StartSpawnOnStart();
    }

    // Initializes the enemy pool by pre-instantiating objects
    void InitializePool()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false); // Deactivate enemy initially
            enemyPool.Add(enemy);
        }
    }

    // Coroutine to spawn enemies at defined intervals
    IEnumerator SpawnEnemies()
    {
        while (canSpawn)
        {
            if (activeEnemies < maxEnemies)
            {
                SpawnEnemy(); // Spawn an enemy if limit is not reached
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    // Activates an enemy from the pool at a random spawn point
    void SpawnEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                enemy.transform.position = spawnPoint.position;
                enemy.SetActive(true); // Activate the enemy
                activeEnemies++;

                return;
            }
        }
    }

    // Handles enemy destruction by deactivating and returning it to the pool
    public void OnEnemyDestroyed(GameObject enemy)
    {
        enemy.SetActive(false);
        activeEnemies--;
    }

    // Starts enemy spawning automatically when the scene loads
    public void StartSpawnOnStart()
    {
        canSpawn = true;
        StartCoroutine(SpawnEnemies());
    }

    // Starts enemy spawning when the player enters a trigger zone
    public void StartsSpawnOnTrigger()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    // Starts enemy spawning when the player interacts with an object
    public void StartsSpawnOnInteraction()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(SpawnEnemies());
        }
    }
}
