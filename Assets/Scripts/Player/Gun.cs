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
    [SerializeField] private Light fireLight;
    [SerializeField] private AudioClip fireClip;

    private float lastFiredTime;
    private int fireDamage;

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

    public void Fire(bool isCritical)
    {
        if (cam == null) 
            return;

        if (Time.time >= lastFiredTime + fireInterval)
        {
            lastFiredTime = Time.time;

            if (isCritical)
                fireDamage = damage + 20;
            else
                fireDamage = damage;

            Vector3 hitPosition = GetHitPos(isCritical);
            StartCoroutine(FireEffect(hitPosition));
        }
    }

    private Vector3 GetHitPos(bool isCritical)
    {
        Vector3 fireDir = firePos.forward;

        if (Physics.Raycast(firePos.position, fireDir, out RaycastHit fireHit, distance, targetLayers))
        {
            // 데미지 적용
            var target = fireHit.collider.GetComponent<IDamagable>();
            if (target != null)
            {
                target.OnDamage(fireDamage, fireHit.point, fireHit.normal, isCritical);
            }
            return fireHit.point;
        }

        // 충돌하지 않으면 최대 거리까지 직진
        return firePos.position + fireDir * distance;
    }

    private IEnumerator FireEffect(Vector3 hitPoint)
    {
        lineRenderer.enabled = true;
        fireLight.enabled = true;

        lineRenderer.SetPosition(0, firePos.position);
        lineRenderer.SetPosition(1, hitPoint);
        audioSouce.PlayOneShot(fireClip);

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
        fireLight.enabled = false;
    }
}
