using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private float distance = 10f;
    [SerializeField] private int damage;
    [SerializeField] private float fireInterval;
    [SerializeField] private LayerMask targetLayers;
    [Space]
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private AudioClip fireClip;

    private float lastFiredTime;

    private AudioSource audioSouce;
    private LineRenderer lineRenderer;
    private Camera cam;

    private void Awake()
    {
        audioSouce = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        cam = Camera.main;
    }

    private void OnEnable()
    {
        lineRenderer.enabled = false;
    }

    public void Fire()
    {
        if (cam == null) 
            return;

        if (Time.time >= lastFiredTime + fireInterval)
        {
            lastFiredTime = Time.time;
            Vector3 hitPosition = GetHitPos();
            StartCoroutine(FireEffect(hitPosition));
        }
    }

    private Vector3 GetHitPos()
    {
        // 마우스 위치 받아오기
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out RaycastHit mouseHit))
        {
            Vector3 fireDir = (mouseHit.point - firePos.position);
            fireDir.y = 0;
            fireDir = fireDir.normalized;

            if (Physics.Raycast(firePos.position, fireDir, out RaycastHit fireHit, distance, targetLayers))
            {
                // 데미지 적용
                var target = fireHit.collider.GetComponent<IDamagable>();
                if (target != null)
                {
                    target.OnDamage(damage, fireHit.point, fireHit.normal);
                }
                return fireHit.point;
            }

            // 충돌하지 않으면 최대 거리
            return firePos.position + fireDir * distance;
        }

        // 마우스 레이캐스트 실패 시 전방으로 발사
        return firePos.position + firePos.forward * distance;
    }

    private IEnumerator FireEffect(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, hitPoint);
        audioSouce.PlayOneShot(fireClip);

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}
