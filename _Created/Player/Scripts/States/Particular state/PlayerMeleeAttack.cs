using DG.Tweening;
using UnityEngine;

public class PlayerMeleeAttack: PlayerBaseState
{
    public bool listening;

    PlayerControlInput _input;
    Animator _animator;
    Transform _transform;
    float _stateCompleted;
    float _finishStateTime = 0.8f;
    int _amountAttack = 4;  // for now has 4 attacks
    int _currentAttack = 1; // start state with attack 1st
    int _hashTrigger = PlayerAniParameter.attackTrigger;
    int _hashInt = PlayerAniParameter.currentAttack;
    public PlayerMeleeAttack(PlayerStateMachine context, PlayerStateFactory factory): base(context, factory) { }
    public override void EnterState()
    {
        _transform = _context.transform;
        _input = _context.input;
        _animator = _context.animator;
        _currentAttack = 1;
        _context.SetMoveDirection(0f, 0f, 0f);
        _animator.SetTrigger(_hashTrigger);
        _animator.SetInteger(_hashInt, _currentAttack);

        // rotate to focus at target
        EnemyInformation target = (EnemyInformation)_context.target;
        Vector3 positionTarget = new Vector3(target.transform.position.x, _transform.position.y, target.transform.position.z);
        Vector3 direction = positionTarget - _transform.position;
        _transform.DORotateQuaternion(Quaternion.LookRotation(direction, _transform.up), 0.5f);
        
        if(_currentAttack == 1) _context.SetMoveDirection(0f, 0f, 0f);
    }
    public override void UpdateState()
    {
        CheckSwitchState();

        _stateCompleted = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (listening)
        {
            if(PlayerAcceptCombo()) ContinueCombo();
            else if (_stateCompleted >= _finishStateTime) EndCombo();
        }
    }
    public override void ExitState()
    {
        _animator.SetInteger(_hashInt, 0);
    }
    public override void CheckSwitchState()
    {
        if (_currentAttack == 0 || _currentAttack > _amountAttack) SwitchState(_factory.OnGround());
        else if (_input.jump) SwitchState(_factory.Jump());
    }
    private bool PlayerAcceptCombo()
    {
        return _input.interact && _context.target.GetType() == typeof(EnemyInformation);
    }
    private void ContinueCombo()
    {
        _currentAttack++;
        listening = false;

        _animator.SetInteger(_hashInt, _currentAttack);
        _context.SetMoveDirection(0f, 0f, 0f);
    }
    private void EndCombo()
    {
        _currentAttack = 0;
        listening = false;
        _animator.SetInteger(_hashInt, 0);
    }
}