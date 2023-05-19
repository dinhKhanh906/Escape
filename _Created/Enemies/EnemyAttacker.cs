
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class EnemyAttacker: MonoBehaviour, IAttacker
{
    public float attackRange;
    public bool attackComplete = false;

    [SerializeField] protected float _coolDown = 1f;
    [SerializeField] protected bool _allowAttack = true;
    [SerializeField] protected EnemyStateMachine _context;

    private void Start()
    {
        _allowAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    protected virtual IEnumerator StartWaitCoolDown()
    {
        _allowAttack = false;

        yield return new WaitForSeconds(_coolDown);

        _allowAttack = true;
    }
    protected virtual void LookAtTarget(Vector3 target)
    {
        Vector3 positionTarget = new Vector3(target.x, transform.position.y, target.z);
        Vector3 direction = positionTarget - transform.position;
        transform.DORotateQuaternion(Quaternion.LookRotation(direction, transform.up), 0.5f);
        //_context.transform.rotation = Quaternion.Lerp(_context.transform.rotation, Quaternion.Euler(positionTarget), 0.5f * Time.deltaTime);
    }
    public abstract bool OnEnterAttack();

    public abstract bool OnStayAttack();

    public abstract bool OnExitAttack();
}
