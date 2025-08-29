using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator animator;
    private PlayerInput input;

    private Camera cam;


    public void Awake()
    {
        animator = GetComponent<Animator>(); 
        input = GetComponent<PlayerInput>();
    }

    public void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        Movement();
        Rotation();
    }

    public void Movement()
    {
        // 이동
        Vector3 camForward = cam.transform.forward; // 카메라 Transform 의 -Z 방향 (파란 축)
        Vector3 camRight = cam.transform.right; // X축 방향 (빨간 축)

        // Y축 제거 및 정규화
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // 입력 방향 계산 (직관적인 매핑)
        Vector3 moveDir = camForward * input.MoveV + camRight * input.MoveH;
        moveDir.Normalize();

        // 월드 좌표계 기준 이동
        Vector3 movement = moveDir * speed * Time.deltaTime;
        transform.position += movement;

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

    public void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPos = hit.point;
            targetPos.y = transform.position.y;

            transform.LookAt(targetPos);
        }
    }
}