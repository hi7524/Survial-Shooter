using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int initObjCount = 50; // 각각 50개씩 만들것

    [SerializeField] private GameObject[] enemyPrfs;

    private List<GameObject> enemyList;

    public Dictionary<string, Queue<GameObject>> enemyPool = new Dictionary<string, Queue<GameObject>>();


    private void Awake()
    {
        InitPool();
    }

    // 풀 초기화
    private void InitPool()
    {
        for (int i = 0; i < enemyPrfs.Length; i++)
        {
            string key = enemyPrfs[i].name;
            var queue = new Queue<GameObject>(initObjCount);

            enemyPool[key] = queue;
            CreatePrfs(initObjCount, enemyPrfs[i].name, enemyPrfs[i]);
        }
    }

    // 옵젝 생성
    private void CreatePrfs(int count, string name, GameObject prf)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prf);
            obj.name = name;
            Debug.Log(name);

            obj.SetActive(false);

            // 여기서 생성하는 오브젝트들은 죽을 때 반환되도록
            Zombie zombie = obj.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.OnDeath += ReturnToPool;
            }

            enemyPool[name].Enqueue(obj);
        }
    }

    // 풀에서 오브젝트 꺼내기
    public GameObject GetFromPool(string objName)
    {
        if (!enemyPool.ContainsKey(objName))
            return null;

        return enemyPool[objName].Dequeue();
    }

    // 풀에 반환하기
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        enemyPool[gameObject.name].Enqueue(gameObject);
    }
}
