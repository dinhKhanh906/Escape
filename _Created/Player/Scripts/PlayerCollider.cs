using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider: MonoBehaviour, IImpact
{
    [HideInInspector] public UnityEvent receiveDamageEvent;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerStateMachine _stateMachine;
    [SerializeField] PlayerInformation _infor;
    public void Impact(float damage)
    {
        if (_infor.heath <= 0) return;

        _infor.heath -= damage;
        _animator.SetTrigger(PlayerAniParameter.impact);
        receiveDamageEvent.Invoke();
        if (_infor.heath <= 0f)
        {
            _animator.SetTrigger(EnemyAniParameter.death);
            _stateMachine.enabled = false;
            return;
        }
        else
        {
            _animator.SetTrigger(EnemyAniParameter.impact);

            // back to default state
            _stateMachine.BackToDefaultState();
        }
    }
}
