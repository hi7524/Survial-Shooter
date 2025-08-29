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
                    capsuleCollider.enabled = false;
                    agent.enabled = false;
                    break;
            }
        }
    }

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
        base.OnEnable();
        capsuleCollider.enabled = true;
        agent.enabled = true;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
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
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        State = Status.Dead;
    }
}
