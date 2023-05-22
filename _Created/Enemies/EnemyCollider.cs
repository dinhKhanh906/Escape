using UnityEngine;
using UnityEngine.Events;

public class EnemyCollider : MonoBehaviour, IReceiveDame
{
    [HideInInspector] public UnityEvent receiveDameEvent;
    [SerializeField] LayerMask selfLayer;
    [SerializeField] EnemyStateMachine _stateMachine;
    [SerializeField] EnemyInformation _infor;
    [SerializeField] Animator _animator;
    private void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Enemy"))
            Debug.LogWarning($"{gameObject.name} is not 'Enemy' layer");
    }
    public void ReceiveDame(float damage)
    {
        if (_infor.heath <= 0) return;

        _infor.heath -= damage;
        receiveDameEvent.Invoke();
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

    
