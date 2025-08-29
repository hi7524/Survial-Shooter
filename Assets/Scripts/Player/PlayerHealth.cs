using UnityEngine;

public class PlayerHealth : Entity 
{
    [SerializeField] private UIManager uiManager;


    public override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDamage(10);
        }
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);

        uiManager.SetHealthBarValue(health / maxHealth);
    }

    protected override void Die()
    {
        base.Die();
    }
}