using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void stopSpawning()
    {
        isSpawning = false;
    }

    public void startSpawning()
    {
        if (spawnedEnemy.Level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    void Update()
    {
    }

    public IEnumerator SpawnEnemies()
    {
        if (isSpawning)
        {
            if (spawnCount == 0)
            {
                spawnCount = defaultSpawnCount;
            }
            int i = spawnCount;
            while (i > 0)
            {
                Enemy enemy = Instantiate(spawnedEnemy);
                enemy.GetComponent<Enemy>().enemySpawner = this;
                enemy.GetComponent<Enemy>().combatManager = combatManager;
                --i;
                spawnCount = i;
                if (combatManager != null)
                {
                    combatManager.totalEnemies++;
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void onDeath()
    {
        totalKill++;
        ++totalKillWave;

        if (totalKillWave == minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            defaultSpawnCount *= spawnCountMultiplier;
            if (spawnCountMultiplier < 3)
                spawnCountMultiplier += multiplierIncreaseCount;
            spawnCount = defaultSpawnCount;
        }
    }
}
