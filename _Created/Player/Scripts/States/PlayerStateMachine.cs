using UnityEngine;
using System;
using System.Collections;
using DG.Tweening;

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

    [Header("Chooser infor")]
    public PlayerDetection chooser;

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
        currentState.FixedUpdateState();
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
        if(x != 0 || y != 0 || z != 0)
        {
            moveDirection = transform.right * x + transform.forward * z + transform.up * y;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        
    }
    public void LookAtTarget()
    {
        // rotate to focus at target
        EnemyInformation target = (EnemyInformation)chooser.currentTarget;
        Vector3 positionTarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        Vector3 direction = positionTarget - transform.position;
        transform.DORotateQuaternion(Quaternion.LookRotation(direction, transform.up), 0.5f);
    }
    public void BackToDefaultState()
    {
        currentState.SwitchState(factory.OnGround());
    }
    public IEnumerator MoveToTargetPoint(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            character.Move((targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // make sure move to exactly point
        character.Move((targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime);
    }
}
