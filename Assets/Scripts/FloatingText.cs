using UnityEngine;
using System;
using System.Collections;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.3f;

    public event Action<FloatingText> OnReturnToPool;

    private TextMesh textMesh;

    private Camera cam;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    public void ShowDamage(int damage, Color color)
    {
        StopAllCoroutines();

        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;

        textMesh.text = damage.ToString();
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(destroyTime);
        OnReturnToPool?.Invoke(this);
    }
}