using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [Header("Singleton")]
    [SerializeField] bool m_destroyOnLoad = false;

    [Space(5)]

    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public float spawnRadius = 20f; // Radius around the center where enemies will spawn
    public float safeZoneRadius = 10f; // Minimum distance from the center (Vector3.zero) to spawn enemies
    public int initialEnemies = 5; // Number of enemies in the first wave
    public float waveInterval = 5f; // Time between waves in seconds

    private int waveNumber = 1; // Current wave number
    private List<Enemy> activeEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (m_destroyOnLoad) { DontDestroyOnLoad(this.gameObject); }
    }

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return SpawnEnemies(initialEnemies * waveNumber);

            yield return new WaitUntil(() => activeEnemies.Count == 0); ;

            yield return new WaitForSeconds(waveInterval);

            // Increase the wave number
            waveNumber++;
        }
    }

    IEnumerator SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = Random.insideUnitSphere * spawnRadius;
                spawnPosition.y = 0f; // Ensure the spawn position is on the ground plane
            } while (spawnPosition.magnitude < safeZoneRadius);

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.5f,2f));
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
