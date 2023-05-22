
public class PlayerBaseState: IBaseState
{
    protected PlayerStateMachine _context;
    protected PlayerStateFactory _factory;
    public PlayerBaseState (PlayerStateMachine context, PlayerStateFactory factory)
    {
        _context = context;
        _factory = factory;
    }
    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void FixedUpdateState() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }
    public virtual void SwitchState(PlayerBaseState newState)
    {
        ExitState();

        newState.EnterState();
        _context.currentState = newState;
    }
}