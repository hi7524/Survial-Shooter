using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveV { get; private set; }
    public float MoveH { get; private set; }
    public bool Fire { get; private set; }

    private void Update()
    {
        MoveV = Input.GetAxis(InputActions.vAxis);
        MoveH = Input.GetAxis(InputActions.hAxis);
        Fire = Input.GetButtonDown(InputActions.fireBtn); // 테스트 위해 임시로 Down으로
    }
}
