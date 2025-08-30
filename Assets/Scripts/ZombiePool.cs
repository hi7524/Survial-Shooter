using System.Collections.Generic;
using UnityEngine;

public enum ZombieType
{
    Bunny,
    Bear,
    Hell,
}

[System.Serializable]
public class ZombiePoolSetting
{
    public ZombieType type;
    public Zombie prefab;
    public int initSize;
    public int maxSize;
}

public class ZombiePool : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] private List<ZombiePoolSetting> prefabSettings;

    private Dictionary<ZombieType, ObjectPool<Zombie>> pools = new();


    private void Awake()
    {
        InitPool();
    }

    public void InitPool()
    {
        for (int i = 0; i < prefabSettings.Count; i++)
        {
            ZombieType type = prefabSettings[i].type;
            Zombie prefab = prefabSettings[i].prefab;
            int initSize = prefabSettings[i].initSize;
            int maxSize = prefabSettings[i].maxSize;

            pools[type] = new ObjectPool<Zombie>(
                prefab, 
                initSize, 
                maxSize,
                onCreate: z => 
                { 
                    z.OnDeath += () => gameManager.AddScore(100);
                    z.DisableEvent += () => Despawn(type, z);
                });
        }
    }

    public Zombie Spawn(ZombieType type, Vector3 pos, Quaternion rot)
    {
        var zombie = pools[type].Get();
        zombie.transform.SetPositionAndRotation(pos, rot);
        zombie.gameObject.SetActive(true);
        return zombie;
    }

    public void Despawn(ZombieType type, Zombie zombie)
    {
        pools[type].Return(zombie);
    }
}