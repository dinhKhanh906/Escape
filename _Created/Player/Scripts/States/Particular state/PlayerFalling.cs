

using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerFalling : PlayerBaseState
{
    PlayerThirdPersonInput _input;
    CharacterController _character;
    float _gravity;
    float _xAxis, _yAxis, _zAxis;
    public PlayerFalling(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory){}
    public override void EnterState()
    {
        _gravity = _context.gravity;
        _input = _context.input;
        _character = _context.character;

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
    public override void FixedUpdateState()
    {
        if (_character.enabled) _character.Move(_context.moveDirection * Time.deltaTime);
    }
    public override void ExitState()
    {
        _context.moveDirection.y = -0.2f;

        if (_context.animator) _context.animator.SetBool(PlayerAniParameter.isGrounded, true);
    }
    public override void CheckSwitchState()
    {
        if (_context.Grounded()) SwitchState(_factory.OnGround());
    }
}