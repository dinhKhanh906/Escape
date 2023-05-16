
public class EnemyBaseState : IBaseState
{
    protected EnemyStateMachine _context;
    protected EnemyStateFactory _factory;
    public EnemyBaseState(EnemyStateMachine context, EnemyStateFactory factory)
    {
        _context = context;
        _factory = factory;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }
    public virtual void SwitchState(EnemyBaseState newState)
    {
        ExitState();

        newState.EnterState();
        _context.currentState = newState;
    }
}
