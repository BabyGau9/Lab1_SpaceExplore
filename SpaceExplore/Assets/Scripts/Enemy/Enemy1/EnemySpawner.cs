using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject EnemyGo1; // this is out enemy prefab
    public GameObject EnemyGo2;
    float maxSpawnRateInSeconds = 4f;
    private Coroutine spawnEnemiesCoroutine;

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float spawnY = max.y;

        int spawnLocation = Random.Range(0, 3);

        float spawnX = spawnLocation == 0 ? min.x : (spawnLocation == 1 ? max.x : (min.x + max.x) / 2);
        // Instantiate an enemy
        int spawnType = Random.Range(0, 3);
        if (spawnType == 0 || spawnType == 2)
        {
            GameObject enemy1 = Instantiate(EnemyGo1);
            enemy1.transform.position = new Vector2(spawnX, spawnY);
            MoveEnemy(enemy1, spawnLocation);
        }
        if (spawnType == 1 || spawnType == 2)
        {
            GameObject enemy2 = Instantiate(EnemyGo2);
            enemy2.transform.position = new Vector2(spawnX, spawnY);
            MoveEnemy(enemy2, spawnLocation);
        }
    }

    void MoveEnemy(GameObject enemy, int spawnLocation)
    {
        float moveSpeed = 2f;
        Vector2 moveDirection;

        if (spawnLocation == 0) 
            moveDirection = Vector2.right;
        else if (spawnLocation == 1) 
            moveDirection = Vector2.left;
        else 
            moveDirection = Vector2.down;

        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            StartCoroutine(MoveEnemyCoroutine(enemy, moveDirection * moveSpeed));
        }
    }

    IEnumerator MoveEnemyCoroutine(GameObject enemy, Vector2 velocity)
    {
        while (enemy != null)
        {
            enemy.transform.position += (Vector3)velocity * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(1f, maxSpawnRateInSeconds));
        }
    }

    IEnumerator IncreaseSpawnRateCoroutine()
    {
        while (maxSpawnRateInSeconds > 1f)
        {
            yield return new WaitForSeconds(30f); // Wait for 30 seconds
            maxSpawnRateInSeconds = Mathf.Max(1f, maxSpawnRateInSeconds - 1f);
        }
    }

    // Function to start enemy spawning
    public void ScheduleEnemySpawner()
    {
        StopAllCoroutines(); // Ensure no previous coroutines are running
        gameObject.SetActive(true);
        maxSpawnRateInSeconds = 4f; // Reset spawn rate
        spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
        StartCoroutine(IncreaseSpawnRateCoroutine());
    }

    // Function to stop enemy spawning
    public void UnscheduleEnemySpawner()
    {
        StopAllCoroutines();
    }

    // Function to pause spawning
    public void PauseSpawning()
    {
        if (spawnEnemiesCoroutine != null)
        {
            StopCoroutine(spawnEnemiesCoroutine);
            spawnEnemiesCoroutine = null;
        }
    }

    // Function to resume spawning
    public void ResumeSpawning()
    {
        if (spawnEnemiesCoroutine == null)
        {
            spawnEnemiesCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
        }
    }
}
