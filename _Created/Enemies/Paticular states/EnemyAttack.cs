
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : EnemyBaseState
{
    public EnemyAttack(EnemyStateMachine context, EnemyStateFactory factory) : base(context, factory){}
    NavMeshAgent _agent;
    Transform _player;
    public override void EnterState()
    {
    }
    public override void UpdateState()
    {
        
    }
}
