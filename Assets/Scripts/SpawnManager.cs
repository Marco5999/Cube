using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
     public GameObject enemyPrefab;  // Reference to the enemy prefab
    public Transform[] spawnPoints; // Array of possible spawn points
    public int enemiesToSpawn = 5;  // Number of enemies to spawn
    public float spawnInterval = 5f;  // Time interval between spawns
    public PointTracker pointTracker; // Reference to the PointTracker script

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Choose a random spawn point from the spawn points array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            // Instantiate the enemy at the spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            // Get the EnemyAI script and assign the pointTracker reference
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.pointTracker = pointTracker;  // Assign PointTracker reference at runtime
            }

            // Optionally, if you want to assign EnemyHealth at runtime as well
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Assign any references or configurations to EnemyHealth if necessary
                // For example: enemyHealth.enemyAI = enemyAI; (if needed)
            }

            // Wait for the spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
