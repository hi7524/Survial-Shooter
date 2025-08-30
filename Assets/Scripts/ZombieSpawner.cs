using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [Space]
    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] Zombie[] zombiePrfs;

    private EnemyPool enemyPool;

    private float lastSpawnTime = 0f;


    private void Start()
    {
        // enemyPool = GetComponent<EnemyPool>();
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

        //GameObject enemy = enemyPool.GetFromPool("ZomBear");

        //if (enemy != null)
        //{
        //    enemy.transform.position = spawnPos;
        //    enemy.SetActive(true);
        //}

        int randomIdx = Random.Range(0, zombiePrfs.Length);
        Zombie obj = Instantiate(zombiePrfs[randomIdx]);
        obj.transform.position = spawnPos;
        obj.OnDeath += () => gameManager.AddScore(100);
    }
}
