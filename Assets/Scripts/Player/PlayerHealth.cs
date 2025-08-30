using UnityEngine;

public class PlayerHealth : Entity 
{
    [SerializeField] private AudioClip hurtClip;
    [Space()]
    [SerializeField] private UIManager uiManager;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDamage(int damage, Vector3 hitPos, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPos, hitNormal);

        uiManager.SetHealthBarValue(health / maxHealth);
        audioSource.PlayOneShot(hurtClip);
        uiManager.PlayDamagedEffect();
    }

    protected override void Die()
    {
        base.Die();
    }
}