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
        Vector3 movement = new Vector3(input.MoveH, 0, input.MoveV) * speed * Time.deltaTime;

        transform.Translate(movement);

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