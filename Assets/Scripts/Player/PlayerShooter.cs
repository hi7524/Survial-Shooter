using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private float critcal;

    private PlayerInput input;


    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.Fire)
        {
            gun.Fire(SetCritical());
        }
    }

    private bool SetCritical()
    {
        float random = Random.value;

        if (random < critcal * 0.01f)
            return true;

        return false;
    }
}
