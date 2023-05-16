

public class PlayerStateFactory
{
    PlayerStateMachine _context;
    public PlayerStateFactory(PlayerStateMachine context) => _context = context;
    public PlayerBaseState OnGround() => new PlayerOnGround(_context, this);
    public PlayerBaseState Jump() => new PlayerJump(_context, this);
    public PlayerBaseState Falling() => new PlayerFalling(_context, this);
    public PlayerBaseState MeleeAttack() => new PlayerMeleeAttack(_context, this);
}