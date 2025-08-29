using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePos;

    [SerializeField] private ParticleSystem fireParticle;

    private LineRenderer lineRenderer;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        lineRenderer.enabled = false;
    }

    private void Shoot()
    {
        Vector3 hitPosition = Vector3.zero;

        // 마우스 위치 받아오기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitMouse;

        if (!Physics.Raycast(ray, out hitMouse))
        {
            Debug.Log("안맞음");
            return;
        }

        // 마우스 위치로 Ray 그려서 충돌 확인하기
        Vector3 fireDir = hitMouse.point - firePos.position;
        fireDir.y = 0;
        fireDir = fireDir.normalized;

        RaycastHit hit;

        // 충돌 위치 방향으로 Ray 쏘기
        if (Physics.Raycast(firePos.position, fireDir, out hit, 50f))
        {
            hitPosition = hit.point;

            var target = hit.collider.GetComponent<IDamagable>();
            if (target != null)
            {
                target.OnDamage(10);
            }
        }
        else
        {
            hitPosition = firePos.position + firePos.forward * 50f;
        }

        StartCoroutine(FireEffect(hitPosition));
    }

    public void Fire()
    {
        //StartCoroutine(FireEffect());
        Shoot();
    }

    private IEnumerator FireEffect(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, hitPoint);

        yield return new WaitForSeconds(0.01f);

        lineRenderer.enabled = false;
    }
}
