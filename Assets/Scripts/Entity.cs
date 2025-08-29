using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth = 100f;
    protected float health;
    protected bool isDead;

    public event Action OnDeath;


    protected virtual void OnEnable()
    {
        health = maxHealth;    
    }

    public virtual void OnDamage(int damage, Vector3 hitPos, Vector3 hitNormal)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        health = 0;
        isDead = true;

        if (OnDeath != null)
        {
            OnDeath();
        }
    }

    public void OnDamage(int damage, System.Numerics.Vector3 hitPos)
    {
        throw new NotImplementedException();
    }
}