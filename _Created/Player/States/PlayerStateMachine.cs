using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("preference")]
    public CharacterController character;
    public Animator animator;
    public PlayerControlInput input;
    [Header("Information")]
    public float moveSpeed;
    public float jumpHeight;
    public LayerMask groundMask;
    public float radiusCheckGround = 0.4f;
    public Transform groundCheck;
    [Range(0f, Mathf.Infinity)] public float gravity;
    public Vector3 moveDirection;
    public PlayerBaseState currentState;
    public PlayerStateFactory factory;

    [Header("Camera")]
    public Transform cam;
    public float turnDensity = 10f;
    Vector3 _targetEuler;

    [Header("Interaction infor")]
    public BaseInforInteraction targetInfor;

    private void Awake()
    {
        factory = new PlayerStateFactory(this);
        currentState = factory.OnGround();
    }
    private void Start()
    {
        currentState.EnterState();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        currentState.UpdateState();
    }
    private void FixedUpdate()
    {
        if(character.enabled) character.Move(moveDirection * Time.deltaTime);
        if(moveDirection.x != 0f || moveDirection.z != 0f)
        {
            _targetEuler = transform.rotation.eulerAngles;
            _targetEuler.y = cam.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetEuler), turnDensity * Time.fixedDeltaTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, radiusCheckGround);
    }
    public bool Grounded()
    {
        if (Physics.CheckSphere(groundCheck.position, radiusCheckGround, groundMask))
        {
            return true;
        }
        else return false;
    }
    public void SetMoveDirection(float x, float y, float z)
    {
        x *= moveSpeed;
        z *= moveSpeed;
        moveDirection = transform.right * x
                        + transform.forward * z
                        + transform.up * y;
    }
}
