using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.3f;

    public event Action<FloatingText> OnReturnToPool;

    private TextMesh textMesh;
    private Animator animator;

    private Camera cam;

    string defaultState = "FloatingText";

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    void OnEnable()
    {
        animator.Rebind();
        animator.Update(0f);
        animator.Play(defaultState, 0, 0f);
    }

    public void ShowDamage(int damage, bool isCritical)
    {
        StopAllCoroutines();

        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;

        textMesh.text = damage.ToString();
        TextSetting(isCritical); // 치명타 여부에 따른 설정값
        StartCoroutine(ReturnToPoolAfterDelay());
    }

    public void TextSetting(bool isCritical)
    {
        if (isCritical)
        {

            textMesh.color = Color.red;
            //animator.SetTrigger("Critical");
        }
        else
        {
            textMesh.color = Color.white;
        }

        animator.SetBool("isCritical", isCritical);
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(destroyTime);
        OnReturnToPool?.Invoke(this);
    }
}