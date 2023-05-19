using Unity.VisualScripting;
using UnityEngine;

public class PlayerOnGround : PlayerBaseState
{
    PlayerControlInput _input;
    float _sprintMultiply, _velocityX, _velocityZ, _aniSpeed;
    Animator _animator;
    int _hashMovement;
    public PlayerOnGround(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) { }
    public override void EnterState()
    {
        _input = _context.input;
        _animator = _context.animator;
        _hashMovement = PlayerAniParameter.speed;
    }
    public override void UpdateState()
    {
        CheckSwitchState();

        if (_input.sprint) _sprintMultiply = 1.5f;
        else _sprintMultiply = 1.0f;

        _velocityX = _input.moveHorizontal * _sprintMultiply;
        _velocityZ = _input.moveVertical * _sprintMultiply;

        if(_velocityX != 0f || _velocityZ != 0f)
        {
            // move left, right, forward -> animation forward
            // move back -> animation backward
            if(_velocityX != 0f || _velocityZ > 0f) _aniSpeed = Mathf.Lerp(_aniSpeed, _sprintMultiply, 5f * Time.deltaTime);
            else if(_velocityZ < 0f) _aniSpeed = Mathf.Lerp(_aniSpeed, -1 * _sprintMultiply, 5f * Time.deltaTime);
        }
        else
        {
            if (Mathf.Abs(_aniSpeed) < 0.1f) _aniSpeed = 0f;
            else _aniSpeed = Mathf.Lerp(_aniSpeed, 0f, 5f * Time.deltaTime);
        }

        if (_animator) _animator.SetFloat(_hashMovement, _aniSpeed);

        _context.SetMoveDirection(_velocityX, 0f, _velocityZ);
    }
    public override void CheckSwitchState()
    {
        if (_input.interact)
        {
            if(_context.target == null)
            {
                Debug.Log("Have no target");
            }
            else if (_context.target.GetType() == typeof(EnemyInformation)) 
                SwitchState(_factory.MeleeAttack());
        }
        if (_input.jump) SwitchState(_factory.Jump());
        else if (!_context.Grounded()) SwitchState(_factory.Falling());
    }

}