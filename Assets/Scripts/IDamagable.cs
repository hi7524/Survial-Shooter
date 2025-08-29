using UnityEngine;

public interface IDamagable
{
    public void OnDamage(int damage, Vector3 hitPos, Vector3 hitNormal);
}