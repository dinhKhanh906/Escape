using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SkeletonAttacker : EnemyAttacker
{
    [SerializeField] Animator _animator;
    [SerializeField] EnemyStateMachine _context;
    public override bool OnEnterAttack()
    {
        if (!_allowAttack) return false;

        _context.agent.isStopped = true;  // stop agent to attack

        // rotate to focus at target
        LookAtTarget(_context.player.position);
        //

        attackComplete = false;
        StartCoroutine(StartWaitCoolDown());
        _animator.SetTrigger(EnemyAniParameter.hit);
        return true;
    }

    public override bool OnExitAttack()
    {
        _context.agent.isStopped = false;   // continue move
        return true;
    }

    public override bool OnStayAttack()
    {
        throw new System.NotImplementedException();
    }
}
