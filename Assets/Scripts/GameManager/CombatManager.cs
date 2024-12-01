using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 3f;
    public int waveNumber = 0;
    public int totalEnemies = 0;
    public int totalPoints = 0;

    // Start is called before the first frame update
    void Start()
    {

        waveNumber = 0;
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.combatManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (totalEnemies <= 0)
        {
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                timer = 0;
                StartNextWave();
            }
        }
    }

    private void StartNextWave()
    {

        timer = 0;
        waveNumber++;
        // Debug.Log("Starting wave " + waveNumber);
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            Debug.Log("Starting enemy spawner");
            enemySpawner.startSpawning();
        }
    }

    public void onDeath()
    {
        totalEnemies--;
        totalPoints++;
    }
}