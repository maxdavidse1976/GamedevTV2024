using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [Header("Singleton")]
    [SerializeField] bool m_destroyOnLoad = false;

    [Header("UI Settings")]
    [SerializeField] TMPro.TMP_Text countdownText; // UI Text element for countdown
    [SerializeField] TMPro.TMP_Text enemyText; // UI Text element for enemy count

    [Space(5)]

    [Header("Enemy Settings")]
    [SerializeField] GameObject[] enemyPrefabs; // Array of enemy prefabs
    [SerializeField] int[] spawnRatios = { 5, 3, 2, 1 }; // Ratios for spawning enemies

    public float spawnRadius = 20f; // Radius around the center where enemies will spawn
    public float safeZoneRadius = 10f; // Minimum distance from the center (Vector3.zero) to spawn enemies
    public int initialEnemies = 3; // Number of enemies in the first wave
    public float waveCountdown = 5f; // Time before wave starts in seconds

    int waveNumber = 0; // Current wave number

    int enemyCount0 = 0;
    int enemyCount1 = 0;
    int enemyCount2 = 0;

    List<Enemy> activeEnemies = new List<Enemy>();

    bool waveStarted;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        if (m_destroyOnLoad) { DontDestroyOnLoad(this.gameObject); }
    }

    void LateUpdate()
    {
        if(waveStarted)
        enemyText.text = "Enemies " + activeEnemies.Count.ToString() + "/" + (initialEnemies + (waveNumber * 2));
    }

    public void StartWave()
    {
        Cursor.visible = false;

        StartCoroutine(SpawnNextWave());
    }

    IEnumerator SpawnNextWave()
    {
        enemyText.text = "";

        enemyCount0 = 0;
        enemyCount1 = 0;
        enemyCount2 = 0;

        waveNumber++;

        // Countdown UI Placeholder
        float countdown = waveCountdown;
        while (countdown > 0)
        {
            countdownText.text = "Next Wave in " + countdown.ToString();

            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }
        countdownText.text = "Wave " + waveNumber.ToString();
        waveStarted = true;
        yield return SpawnEnemies(initialEnemies + (waveNumber * 2));

        yield return new WaitUntil(() => activeEnemies.Count == 0);

        EndWave();
    }

    IEnumerator SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = Random.insideUnitSphere * spawnRadius;
                spawnPosition.y = 0f;
            } while (spawnPosition.magnitude < safeZoneRadius);

            int prefabIndex = GetNextPrefabIndex();
            //GameObject enemy = Instantiate(enemyPrefabs[prefabIndex], spawnPosition, Quaternion.identity);
            GameObject enemy = PoolManager.Instance.SpawnFromPool("Enemy_"+prefabIndex, spawnPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    int GetNextPrefabIndex()
    {
        if (enemyCount0 < spawnRatios[0])
        {
            enemyCount0++;
            if (enemyCount0 == spawnRatios[0])
            {
                enemyCount0 = 0;
                enemyCount1++;
                return 1;
            }
            return 0;
        }
        else if (enemyCount1 < spawnRatios[1])
        {
            enemyCount1++;
            if (enemyCount1 == spawnRatios[1])
            {
                enemyCount1 = 0;
                enemyCount2++;
                return 2;
            }
            return 1;
        }
        else if (enemyCount2 < spawnRatios[2])
        {
            enemyCount2++;
            if (enemyCount2 == spawnRatios[2])
            {
                enemyCount2 = 0;
                return 3;
            }
            return 2;
        }
        else
        {
            return 0;
        }
    }

    void EndWave()
    {
        waveStarted = false;

        UIManager.Instance.ShowUpgradeScreen();

        Cursor.visible = true;
        //UpgradesManager upgradesManager = FindObjectOfType<UpgradesManager>();
        //UpgradesManager.Instance.ProvideRandomUpgrades();
        //StartWave();
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
