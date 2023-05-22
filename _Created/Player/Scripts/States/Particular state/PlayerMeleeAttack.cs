﻿using UnityEngine;

public class PlayerMeleeAttack: PlayerBaseState
{
    public bool listening;

    PlayerControlInput _input;
    Animator _animator;
    float _stateCompleted;
    float _finishStateTime = 0.8f;
    int _amountAttack = 4;  // for now has 4 attacks
    int _currentAttack = 1; // start state with attack 1st
    int _hashTrigger = PlayerAniParameter.attackTrigger;
    int _hashInt = PlayerAniParameter.currentAttack;
    public PlayerMeleeAttack(PlayerStateMachine context, PlayerStateFactory factory): base(context, factory) { }
    public override void EnterState()
    {
        _input = _context.input;
        _animator = _context.animator;
        _currentAttack = 1;
        _animator.SetTrigger(_hashTrigger);
        _animator.SetInteger(_hashInt, _currentAttack);
        
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
        // check player accept continue click to attacck
        // and player grounded
        return _input.interact && _context.chooser.currentTarget.GetType() == typeof(EnemyInformation) && _context.Grounded();
    }
    private void SwitchToNextAnimation()
    {
        _currentAttack = _animator.GetInteger(_hashInt);

        _currentAttack++;
        _animator.SetInteger(_hashInt, _currentAttack);
    }
    private void ContinueCombo()
    {
        SwitchToNextAnimation();

        listening = false;
    }
    private void EndCombo()
    {
        _currentAttack = 0;
        _animator.SetInteger(_hashInt, 0);
        listening = false;
    }
}