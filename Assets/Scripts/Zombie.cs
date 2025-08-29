using UnityEngine;
using UnityEngine.AI;

public class Zombie : Entity
{
    private NavMeshAgent agent;
    private Animator animator;

    private Transform target;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }


    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        Debug.Log("데미지 입음");
    }

    protected override void Die()
    {
        base.Die();
    }
}
