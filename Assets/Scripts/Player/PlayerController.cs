using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] private float speed;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float rotateStopDistance = 0.2f;

    private Animator animator;
    private PlayerInput input;

    private Camera cam;
    private PlayerHealth health;


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
        if (gameManager.IsPaused || gameManager.IsGameOver)
            return;

        Movement();
        Rotation();
    }

    public void Movement()
    {
        // 이동
        Vector3 camForward = cam.transform.forward; // 카메라 Transform 의 -Z 방향 (파란 축)
        Vector3 camRight = cam.transform.right; // X축 방향 (빨간 축)

        // Y축 제거 및 정규화
        // 카메라에 롤(삐딱하게 기운 회전)이 있을 때도 정확히 직교하도록 만드는 방법
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * input.MoveV + camRight * input.MoveH;
        moveDir.Normalize();

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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask))
        {
            Vector3 targetPos = hit.point;
            targetPos.y = transform.position.y;

            if (Vector3.Distance(targetPos, transform.position) > rotateStopDistance)
            {
                transform.LookAt(targetPos);
            }
        }

        // Ray가 자신의 콜라이더를 맞아서 스스로의 Transform.Position을 바라보려다 떨림 현상 발생
        // -> 때문에 너무 가까운 점은 회전하지 않고, LayerMask를 통해 필터링하여 바닥만 찍도록 할 것
    }
}