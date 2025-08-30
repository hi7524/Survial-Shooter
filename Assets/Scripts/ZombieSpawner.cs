using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [Space]
    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] Zombie[] zombiePrfs;
    [SerializeField] Transform[] spawnTrans;

    private EnemyPool enemyPool;

    private float lastSpawnTime = 0f;


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
        // 랜덤 위치 설정
        int random = Random.Range(0, spawnTrans.Length);
        Transform spawnPos = spawnTrans[random];

        // 랜덤 종류 설정
        int randomIdx = Random.Range(0, zombiePrfs.Length);
        Zombie obj = Instantiate(zombiePrfs[randomIdx]);
        obj.transform.position = spawnPos.position;
        obj.OnDeath += () => gameManager.AddScore(100);
    }
}
