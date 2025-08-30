using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [Space]
    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] Transform[] spawnTrans;

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
        //int randomIdx = Random.Range(0, zombiePrfs.Length);
        zombiePool.Spawn(ZombieType.Bear, spawnPos, Quaternion.identity);
    }
}
