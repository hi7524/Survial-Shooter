using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth = 100f;
    protected float health;
    protected bool isAlive;

    public event Action OnDeath;


    public virtual void OnEnable()
    {
        health = maxHealth;    
    }

    public virtual void OnDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        health = 0;
        isAlive = false;

        if (OnDeath != null)
        {
            OnDeath();
        }
    }
}