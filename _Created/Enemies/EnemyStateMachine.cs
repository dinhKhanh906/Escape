
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyStateMachine: MonoBehaviour
{
    [Header("preference")]
    public EnemyInformation infor;
    public Animator animator;
    public NavMeshAgent agent;
    [Header("Information")]
    public EnemyStateFactory factory;
    public EnemyBaseState currentState;
    [Header("Patrol setup")]
    public bool readyToFindPlayer;
    public Transform[] destinationPoints;
    public float playerDetection;
    private void Awake()
    {
        factory = new EnemyStateFactory(this);
        agent.speed = infor.speed;
        currentState = factory.Patrol();
    }
    private void Start()
    {
        currentState.EnterState();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetection);
    }
    private void Update()
    {
        currentState.UpdateState();
    }
    public void BackToDefaultState()
    {
        currentState.SwitchState(factory.Patrol());
    }
    public void MoveToTargetPoint(Vector3 targetPosition, float duration)
    {
        transform.DOMove(targetPosition, duration).SetEase(Ease.Linear);
    }
}
