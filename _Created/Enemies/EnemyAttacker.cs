
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class EnemyAttacker: MonoBehaviour, IAttacker
{
    public float attackRange;
    public bool attackComplete = false;
    public Transform player;
    public float distance2player { get => Vector3.Distance(player.position, transform.position); }

    [SerializeField] protected float _coolDown = 1f;
    [SerializeField] protected bool _allowAttack = true;
    [SerializeField] protected EnemyStateMachine _context;

    protected Vector3 _directFromPlayer;
    protected RaycastHit _hit;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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
        _context.transform.DORotateQuaternion(Quaternion.LookRotation(direction, transform.up), 0.3f);
        //_context.transform.rotation = Quaternion.Lerp(_context.transform.rotation, Quaternion.Euler(positionTarget), 0.5f * Time.deltaTime);
    }
    public bool CanAttackPlayer()
    {
        if (distance2player >= attackRange) return false;
        
        //
        //_directFromPlayer = (player.transform.position - transform.position).normalized;
        //if (Physics.Raycast(player.position + Vector3.up, _directFromPlayer, out _hit, distance2player + 5f))
        //{
        //    Debug.Log(_hit.transform.name);
        //    if (_hit.transform != transform) return false;
        //    else return true;
        //}
        else return true;
    }
    public abstract bool OnEnterAttack();

    public abstract bool OnStayAttack();

    public abstract bool OnExitAttack();
}
