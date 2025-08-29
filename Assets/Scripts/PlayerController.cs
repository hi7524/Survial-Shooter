using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator animator;
    private PlayerInput input;


    public void Awake()
    {
        animator = GetComponent<Animator>(); 
        input = GetComponent<PlayerInput>();
    }

    public void Update()
    {
        Movement();
    }

    public void Movement()
    {
        // 이동
        Vector3 movement = new Vector3(input.MoveH, 0, input.MoveV) * speed * Time.deltaTime;
        transform.Translate(movement);

        // 애니메이션
        if (input.MoveH != 0 || input.MoveV != 0)
        {
            animator.SetBool(AnimParams.MoveHash, true);
        }
        else
        {
            animator.SetBool(AnimParams.MoveHash, false);
        }
    }
}