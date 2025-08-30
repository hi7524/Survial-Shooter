using System;
using UnityEngine;

// 이벤트 버스(Event Bus) 패턴 !!!!!!

// 발행 측(어디서든 호출 가능)
public static class FloatingTextBus
{
    public static event Action<Vector3, int, bool> OnRequest;

    // 다른 곳에서 Show 호출하면 OnRequest에 담겨있는 실행
    public static void Show(Vector3 worldPos, int damg, bool isCritical)
        => OnRequest?.Invoke(worldPos, damg, isCritical);
}

public class FloatingTextPool : MonoBehaviour
{
    [Header("Prefab & Pool Size")]
    [SerializeField] private FloatingText prefab;
    [SerializeField] private int initialSize = 64;
    [SerializeField] private int maxSize = 256;

    private ObjectPool<FloatingText> pool;

    private void OnEnable()
    {
        pool = new ObjectPool<FloatingText>(prefab, initialSize, maxSize, OnCreate);

        // 이벤트 버스 구독
        // 즉 이거 호출할 때 마다 실행될 메서드들 다 담아두는 것
        // 다른 메서드에서는 안에 뭐 있는지 몰라도 그냥 호출만 하면 됨
        FloatingTextBus.OnRequest += SpawnText;
    }

    private void OnDisable()
    {
        // 구독 해제 (메모리 누수/중복 호출 방지)
        FloatingTextBus.OnRequest -= SpawnText;
    }

    private void OnCreate(FloatingText ft)
    {
        ft.gameObject.SetActive(false);
        ft.OnReturnToPool += ReturnToPool;
    }

    // 이벤트 버스로부터 요청을 받아 실제 스폰 처리
    private void SpawnText(Vector3 pos, int damage, bool isCritical)
    {
        var ft = pool.Get();

        if (ft.transform.parent != null)
            ft.transform.SetParent(null, true);

        ft.transform.position = pos;
        ft.gameObject.SetActive(true);
        ft.ShowDamage(damage, isCritical);
    }

    private void ReturnToPool(FloatingText ft)
    {
        pool.Return(ft);
        ft.gameObject.SetActive(false);
    }
}

/*
"플로팅 텍스트" 기능을 구현하려고 함
이 플로팅 텍스트는 정말 많이 필요한 객체니까 오브젝트 풀링으로 최적화하려 함 
그런데 플로팅 텍스트를 띄우기 위해서는 모든 객체들이 플로팅 텍스트의 오브젝트 풀을 참조해야하는 상황 발생
이 오브젝트들은 프리팹이기때문에 씬에 있는 플로팅 텍스트 오브젝트 풀을 참조하려면 Find를 써야함 
-> 최적화를 하려 했지만 비효율적인 상황 발생

이를 해결하기 위해 이벤트 버스 패턴 이용해보기
 */