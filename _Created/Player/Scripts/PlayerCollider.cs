
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
        if (gameObject.layer != LayerMask.NameToLayer("Player"))
            Debug.LogWarning($"{gameObject.name} is not 'Player' layer");
    }
    public void ReceiveDame(float damage)
    {
        if (_infor.Heath <= 0) return;

        _infor.Heath -= damage;
        receiveDameEvent.Invoke();
        if (_infor.Heath <= 0f)
        {
            _animator.SetTrigger(PlayerAniParameter.death);
            GameState.instance.Gameover();
            return;
        }
        else
        {
            _animator.SetTrigger(PlayerAniParameter.impact);
            // move character to backward a little
            Vector3 targetBackward = _stateMachine.transform.position - _stateMachine.transform.forward * 0.6f;
            StartCoroutine(_stateMachine.MoveToTargetPoint(targetBackward, 0.5f));
            // back to default state
            _stateMachine.BackToDefaultState();
        }
    }
}
