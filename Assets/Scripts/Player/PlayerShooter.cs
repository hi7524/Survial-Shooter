using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Gun gun;

    private PlayerInput input;


    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.Fire)
        {
            gun.Fire();
        }
    }
}
