
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine: MonoBehaviour
{
    [Header("preference")]
    public EnemyInformation infor;
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
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
        player = FindObjectOfType<PlayerStateMachine>().transform;
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
}
