using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class WaveManager : MonoBehaviour , IWaveControl
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float waveDelay = 5f;

    private int currentWave = 1;
    private int enemiesAlive = 0;
    private bool isRunning = true;
    private Dictionary<GameObject, ObjectPool<EnemyBase>> enemyPools = new();

    [Header("Arena Settings")]
    [SerializeField] private Vector3 arenaCenter = Vector3.zero;
    [SerializeField] private Vector2 arenaSize = new Vector2(90f, 90f);
    public int CurrentWave => currentWave;
    public int EnemiesAlive => enemiesAlive;
    public bool IsRunning => isRunning;
    private void Start()
    {
        foreach (var prefab in enemyPrefabs)
        {
            var pool = new ObjectPool<EnemyBase>(prefab.GetComponent<EnemyBase>(), 70, transform);
            enemyPools[prefab] = pool;
        }

        StartCoroutine(SpawnWave());
    }
    private int CalculateEnemyCount(int wave)
    {
        if(wave < 4) return 30 + (currentWave - 1) * 20;
        return 70 + (wave - 3) * 10;
    }
    private IEnumerator SpawnWave()
    {
        int enemyCount = CalculateEnemyCount(currentWave);
        if (enemiesAlive > 0)
        {
            enemyCount -= enemiesAlive;
        }
        enemiesAlive += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnRandomEnemy();
            yield return null; // yield per frame to avoid freezing on WebGL
        }
    }

    private void SpawnRandomEnemy()
    {
        var prefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)];
        var pool = enemyPools[prefab];
        var enemy = pool.Get();
        enemy.PoolReference = pool;
        enemy.Initialize();
        Vector3 spawnPosition = GetRandomPositionInArena();
        enemy.transform.position = spawnPosition;

        enemy.OnEnemyKilled = () =>
        {
            pool.ReturnToPool(enemy);
            enemiesAlive--;
            if (enemiesAlive <= 0 && !btnClicked && isRunning)
                StartCoroutine(NextWaveDelay());
        };
    }
    private Vector3 GetRandomPositionInArena()
    {
        float randomX = Random.Range(-arenaSize.x / 2f, arenaSize.x / 2f);
        float randomZ = Random.Range(-arenaSize.y / 2f, arenaSize.y / 2f);
        return arenaCenter + new Vector3(randomX, 1f, randomZ);
    }

    private IEnumerator NextWaveDelay()
    {
        yield return new WaitForSeconds(waveDelay);
        currentWave++;
        StartCoroutine(SpawnWave());
    }
    // ========== IWaveControl Implementation ==========
    public void StopWaves() => isRunning = false;

    public void ResumeWaves()
    {
        if (!isRunning)
        {
            isRunning = true;
            if (enemiesAlive <= 0)
                StartCoroutine(NextWaveDelay());
        }
    }

    public void ForceNextWave()
    {
        StopAllCoroutines();
        currentWave++;
        StartCoroutine(SpawnWave());
    }
    private bool btnClicked = false; 

    public void DestroyCurrentWave()
    {
        btnClicked = true;

        foreach (var enemy in FindObjectsOfType<EnemyBase>())
        {
            enemy.OnClick();
        }

        enemiesAlive = 0;

        btnClicked = false;

        if (isRunning)
            StartCoroutine(NextWaveDelay());
    }


}
