using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveV { get; private set; }
    public float MoveH { get; private set; }
    public bool Fire { get; private set; }

    public GameManager gameManager;

    private void Update()
    {
        if (gameManager.IsPaused)
            return;

        MoveV = Input.GetAxis(InputActions.vAxis);
        MoveH = Input.GetAxis(InputActions.hAxis);
        Fire = Input.GetButton(InputActions.fireBtn);
    }
}
