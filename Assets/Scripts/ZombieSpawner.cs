using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [Header("Zombie")]
    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] Transform[] spawnTrans;

    [Header("Zombie Type Probability")]
    [SerializeField] int bunnyP;
    [SerializeField] int bearP;
    [SerializeField] int hellP;

    private float lastSpawnTime = 0f;

    private ZombiePool zombiePool;


    private void Start()
    {
        zombiePool = GetComponent<ZombiePool>();
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
        // 랜덤 위치 설정
        int random = Random.Range(0, spawnTrans.Length);
        Vector3 spawnPos = spawnTrans[random].position;

        // 랜덤 종류 설정
        int randomIdx = Random.Range(0, 100);
        ZombieType type;

        if (randomIdx < bunnyP)
        {
            type = ZombieType.Bunny;
        }
        else if (randomIdx < bunnyP + bearP)
        {
            type = ZombieType.Bear;
        }
        else
        {
            type = ZombieType.Hell;
        }

        zombiePool.Spawn(type, spawnPos, Quaternion.identity);
    }
}
