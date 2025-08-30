using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth = 100f;
    protected float health;
    protected bool isDead;

    public event Action OnDeath;
    public event Action DisableEvent;


    protected virtual void OnEnable()
    {
        health = maxHealth;
        isDead = false;
    }

    public virtual void OnDamage(int damage, Vector3 hitPos, Vector3 hitNormal, bool isCritical = false)
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

    protected virtual void OnDisable()
    {
        if (DisableEvent != null)
        {
            DisableEvent();
        }
    }
}