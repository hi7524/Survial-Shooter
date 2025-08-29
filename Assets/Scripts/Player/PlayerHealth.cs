using UnityEngine;

public class PlayerHealth : Entity 
{
    [SerializeField] private UIManager uiManager;


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamage(10, Vector3.zero, Vector3.zero);
        }
    }

    public override void OnDamage(int damage, Vector3 hitPos, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPos, hitNormal);

        uiManager.SetHealthBarValue(health / maxHealth);
    }

    protected override void Die()
    {
        base.Die();
    }
}