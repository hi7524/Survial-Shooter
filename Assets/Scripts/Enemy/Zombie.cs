using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Entity
{
    private enum Status
    {
        Idle,
        Trace,
        Attack,
        Dead
    }

    private Status state;
    private Status State
    {
        get { return state; }
        set
        {
            state = value;

            switch (state)
            {
                case Status.Idle:
                    agent.isStopped = true;
                    animator.SetBool(AnimParams.MoveHash, false);
                    break;

                case Status.Trace:
                    agent.isStopped = false;
                    animator.SetBool(AnimParams.MoveHash, true);
                    break;

                case Status.Attack:
                    break;

                case Status.Dead:
                    agent.isStopped = true;
                    animator.SetTrigger(AnimParams.DieHash);
                    break;
            }
        }
    }

    [SerializeField] ZombieData data;
    [SerializeField] ParticleSystem hitParticle;

    public int score { get { return data.addScore; } }

    private float lastAttackTime;

    private NavMeshAgent agent;
    private Animator animator;
    private CapsuleCollider capsuleCollider;

    private Transform target;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected override void OnEnable()
    {
        maxHealth = data.maxHealth;
        base.OnEnable();

        target = GameObject.FindGameObjectWithTag(Tags.Player).transform;

        capsuleCollider.enabled = true;
        agent.enabled = true;

        State = Status.Idle;
    }

    private void Update()
    {
        switch (state)
        { 
            case Status.Idle:
                UpdateIdle();
                break;

            case Status.Trace:
                UpdateTrace();
                break;

            case Status.Attack:
                UpdateAttack();
                break;

            case Status.Dead:
                break;
        }
    }

    private void UpdateIdle()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            State = Status.Trace;
        }
    }

    private void UpdateTrace()
    {
        agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) <= data.attackDistance)
        {
            State = Status.Attack;
        }
    }

    private void UpdateAttack()
    {
        if (target == null)
        {
            State = Status.Idle;
            return;
        }

        if (Vector3.Distance(transform.position, target.position) > data.attackDistance)
        {
            State = Status.Trace;
        }

        if (Time.time >= data.attackInterval + lastAttackTime)
        {
            lastAttackTime = Time.time;

            target.GetComponent<IDamagable>().OnDamage(10, Vector3.zero, Vector3.zero);
        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
    }

    public override void OnDamage(int damage, Vector3 hitPos, Vector3 hitNormal, bool isCritical)
    {
        base.OnDamage(damage, hitPos, hitNormal);

        hitParticle.transform.position = hitPos;
        hitParticle.transform.forward = hitNormal;
        hitParticle.Play();

        FloatingTextBus.Show(hitPos + hitNormal * 0.02f, damage, isCritical);
    }

    protected override void Die()
    {
        base.Die();
        State = Status.Dead;
    }

    public void StartSinking()
    {
        capsuleCollider.enabled = false;
        agent.enabled = false;
        StartCoroutine(DelayedActiveFalse());
    }

    private IEnumerator DelayedActiveFalse()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}