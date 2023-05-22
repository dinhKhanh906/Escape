

using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class PlayerJump : PlayerBaseState
{
    PlayerControlInput _input;
    CharacterController _character;
    Transform _transform;
    Vector3 _position;
    float _gravity;
    float _jumpForce;
    float _xAxis, _yAxis, _zAxis;
    public PlayerJump(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory){}

    public override void EnterState()
    {
        _gravity = _context.gravity;
        _input = _context.input;
        _character = _context.character;
        _transform = _context.transform;
        _jumpForce = _context.jumpHeight;

        if (_context.animator)
        {
            _context.animator.SetTrigger(PlayerAniParameter.jump);
            _context.animator.SetBool(PlayerAniParameter.isGrounded, false);
        }
    }
    public override void UpdateState()
    {
        CheckSwitchState();
        _jumpForce -= _gravity * Time.deltaTime;
        _xAxis = _input.moveHorizontal;
        _zAxis = _input.moveVertical;
        _yAxis = _jumpForce;
        _context.SetMoveDirection(_xAxis, _yAxis, _zAxis);
    }
    public override void FixedUpdateState()
    {
        if (_character.enabled) _character.Move(_context.moveDirection * Time.deltaTime);
    }
    public override void CheckSwitchState()
    {
        if (_context.moveDirection.y < 0f) SwitchState(_factory.Falling());
    }
}