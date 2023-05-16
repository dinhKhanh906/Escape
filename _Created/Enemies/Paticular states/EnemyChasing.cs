
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasing : EnemyBaseState
{
    public EnemyChasing(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory){}
    NavMeshAgent _agent;
    NavMeshPath _path = new NavMeshPath();
    Transform _player;
    bool _isLostPlayer;
    public override void EnterState()
    {
        _agent = _context.agent;
        _player = _context.player;
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
        Debug.Log("continue patrol");
        _context.readyToFindPlayer = false;
    }
    public override void CheckSwitchState()
    {
        if (_isLostPlayer) SwitchState(_factory.Patrol());
    }
}
