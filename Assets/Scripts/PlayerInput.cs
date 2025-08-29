using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveV { get; private set; }
    public float MoveH { get; private set; }

    private void Update()
    {
        MoveV = Input.GetAxis(InputActions.vAxis);
        MoveH = Input.GetAxis(InputActions.hAxis);
    }
}
