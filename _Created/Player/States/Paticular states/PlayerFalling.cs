

using UnityEngine;

public class PlayerFalling : PlayerBaseState
{
    PlayerControlInput _input;
    float _gravity;
    float _xAxis, _yAxis, _zAxis;
    public PlayerFalling(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory){}
    public override void EnterState()
    {
        _gravity = _context.gravity;
        _input = _context.input;

        if (_context.animator) _context.animator.SetBool(PlayerAniParameter.isGrounded, false);
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        _xAxis = _input.moveHorizontal;
        _zAxis = _input.moveVertical;
        _yAxis -= _gravity * Time.deltaTime;
        _context.SetMoveDirection(_xAxis, _yAxis, _zAxis);
    }
    public override void ExitState()
    {
        _context.moveDirection.y = -0.2f;
        Debug.Log("falling -> ground");

        if (_context.animator) _context.animator.SetBool(PlayerAniParameter.isGrounded, true);
    }
    public override void CheckSwitchState()
    {
        if (_context.Grounded()) SwitchState(_factory.OnGround());
    }
}