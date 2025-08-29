using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval = 1.0f;
    private float lastSpawnTime = 0f;
    
    private EnemyPool enemyPool;



    private void Start()
    {
        enemyPool = GetComponent<EnemyPool>();
        SpawnEnemy();

    }

    private void Update()
    {
        if (lastSpawnTime + spawnInterval <= Time.time)
        {
            lastSpawnTime = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = Vector3.zero;

        GameObject enemy = enemyPool.GetFromPool("ZomBear");

        if (enemy != null)
        {
            enemy.transform.position = spawnPos;
            enemy.SetActive(true);
        }
    }
}
