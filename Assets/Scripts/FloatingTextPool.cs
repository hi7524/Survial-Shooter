using System;
using UnityEngine;

// �̺�Ʈ ����(Event Bus) ���� !!!!!!

// ���� ��(��𼭵� ȣ�� ����)
public static class FloatingTextBus
{
    public static event Action<Vector3, int, bool> OnRequest;

    // �ٸ� ������ Show ȣ���ϸ� OnRequest�� ����ִ� ����
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

        // �̺�Ʈ ���� ����
        // �� �̰� ȣ���� �� ���� ����� �޼���� �� ��Ƶδ� ��
        // �ٸ� �޼��忡���� �ȿ� �� �ִ��� ���� �׳� ȣ�⸸ �ϸ� ��
        FloatingTextBus.OnRequest += SpawnText;
    }

    private void OnDisable()
    {
        // ���� ���� (�޸� ����/�ߺ� ȣ�� ����)
        FloatingTextBus.OnRequest -= SpawnText;
    }

    private void OnCreate(FloatingText ft)
    {
        ft.gameObject.SetActive(false);
        ft.OnReturnToPool += ReturnToPool;
    }

    // �̺�Ʈ �����κ��� ��û�� �޾� ���� ���� ó��
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
"�÷��� �ؽ�Ʈ" ����� �����Ϸ��� ��
�� �÷��� �ؽ�Ʈ�� ���� ���� �ʿ��� ��ü�ϱ� ������Ʈ Ǯ������ ����ȭ�Ϸ� �� 
�׷��� �÷��� �ؽ�Ʈ�� ���� ���ؼ��� ��� ��ü���� �÷��� �ؽ�Ʈ�� ������Ʈ Ǯ�� �����ؾ��ϴ� ��Ȳ �߻�
�� ������Ʈ���� �������̱⶧���� ���� �ִ� �÷��� �ؽ�Ʈ ������Ʈ Ǯ�� �����Ϸ��� Find�� ����� 
-> ����ȭ�� �Ϸ� ������ ��ȿ������ ��Ȳ �߻�

�̸� �ذ��ϱ� ���� �̺�Ʈ ���� ���� �̿��غ���
 */