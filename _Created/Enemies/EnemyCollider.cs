using UnityEngine;
using UnityEngine.Events;

public class EnemyCollider : MonoBehaviour, IImpact
{
    [HideInInspector] public UnityEvent receiveDameEvent;

    [SerializeField] EnemyStateMachine _stateMachine;
    [SerializeField] EnemyInformation _control;
    [SerializeField] Animator _animator;
    public void Impact(float damage)
    {
        if (_control.heath <= 0f) return;

        receiveDameEvent.Invoke();
        _control.heath -= damage;
        if(_control.heath <= 0f)
        {
            _animator.SetTrigger(EnemyAniParameter.death);
            _stateMachine.enabled = false;
            return;
        }
        else
        {
            _animator.SetTrigger(EnemyAniParameter.impact);
            _stateMachine.BackToDefaultState();
        }
    }
}

    
