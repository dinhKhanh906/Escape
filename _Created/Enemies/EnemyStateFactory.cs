
public class EnemyStateFactory
{
    EnemyStateMachine _context;
    public EnemyStateFactory(EnemyStateMachine context) => _context = context;
    public EnemyBaseState Patrol() => new EnemyPatrol(_context, this);
    public EnemyBaseState Chasing() => new EnemyChasing(_context, this);
    public EnemyBaseState Attack() => new EnemyAttackState(_context, this);
}
