
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasing : EnemyBaseState
{
    public EnemyChasing(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory){}
    EnemyAttacker _attacker;
    NavMeshAgent _agent;
    NavMeshPath _path = new NavMeshPath();
    Transform _transform;
    Transform _player;
    bool _isLostPlayer;
    public override void EnterState()
    {
        _agent = _context.agent;
        _transform = _context.transform;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _attacker = _context.GetComponent<EnemyAttacker>();
        _context.animator.SetBool(EnemyAniParameter.isWalking, true);
    }
    public override void UpdateState()
    {
        CheckSwitchState();

        _agent.CalculatePath(_player.position, _path);
        _isLostPlayer = _path.status == NavMeshPathStatus.PathPartial;
        if (!_isLostPlayer) _agent.SetDestination(_player.position);
    }
    public override void ExitState()
    {
        _context.readyToFindPlayer = false;
        _context.animator.SetBool(EnemyAniParameter.isWalking, false);
    }
    public override void CheckSwitchState()
    {
        if (_isLostPlayer) SwitchState(_factory.Patrol());
        if (_attacker.CanAttackPlayer()) SwitchState(_factory.Attack());
    }
}
