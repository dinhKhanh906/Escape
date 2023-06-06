
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory){}
    NavMeshAgent _agent;
    Transform _player;
    EnemyAttacker _attacker;
    public override void EnterState()
    {
        bool checkEnterAttack = false;
        _attacker = _context.GetComponent<EnemyAttacker>();
        _agent = _context.GetComponent<NavMeshAgent>();
        if (_attacker == null) Debug.LogWarning($"{_agent.gameObject.name} does not have attacker");
        else checkEnterAttack = _attacker.OnEnterAttack();

        // if enter attack successful
        if(!checkEnterAttack)
        {
            SwitchState(_factory.Chasing());
        }
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        _attacker.OnStayAttack();
    }
    public override void ExitState()
    {
        _attacker.OnExitAttack();
    }
    public override void CheckSwitchState()
    {
        if (_attacker.attackComplete) SwitchState(_factory.Chasing());
    }
}
