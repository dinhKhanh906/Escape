using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider: MonoBehaviour, IReceiveDame
{
    [HideInInspector] public UnityEvent receiveDameEvent;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerStateMachine _stateMachine;
    [SerializeField] PlayerInformation _infor;
    private void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Player collider"))
            Debug.LogWarning($"{gameObject.name} is not 'Player collider' layer");
    }
    public void ReceiveDame(float damage)
    {
        if (_infor.heath <= 0) return;

        _infor.heath -= damage;
        receiveDameEvent.Invoke();
        if (_infor.heath <= 0f)
        {
            _animator.SetTrigger(PlayerAniParameter.death);
            _stateMachine.enabled = false;
            return;
        }
        else
        {
            _animator.SetTrigger(PlayerAniParameter.impact);

            // back to default state
            _stateMachine.BackToDefaultState();
        }
    }
}
