
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasing : EnemyBaseState
{
    public EnemyChasing(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory){}
    EnemyAttacker _attacker;
    NavMeshAgent _agent;
    NavMeshPath _path = new NavMeshPath();
    Transform _player;
    Transform _transform;
    bool _isLostPlayer;
    public override void EnterState()
    {
        _agent = _context.agent;
        _transform = _context.transform;
        _player = _context.player;
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
        if (Vector3.Distance(_player.position, _transform.position) <= _attacker.attackRange) SwitchState(_factory.Attack());
    }
}
