using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly Stack<T> available; // 풀에 보관할 오브젝트들
    private readonly int maxSize;
    private readonly T prefab;

    private readonly Action<T> onCreate;

    public ObjectPool(T prefab, int initialSize, int maxSize, Action<T> onCreate = null)
    {
        this.prefab = prefab;
        this.maxSize = maxSize;

        this.onCreate = onCreate;

        available = new Stack<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            available.Push(CreateNew());
        }
    }

    public T Get()
    {
        return available.Count > 0 ? available.Pop() : CreateNew();
    }

    public void Return(T item)
    {
        item.gameObject.SetActive(false);

        if (available.Count < maxSize)
            available.Push(item);
        else
            UnityEngine.Object.Destroy(item.gameObject);
    }

    private T CreateNew()
    {
        var obj = UnityEngine.Object.Instantiate(prefab);
        obj.gameObject.SetActive(false);
        onCreate?.Invoke(obj);
        return obj;
    }
}