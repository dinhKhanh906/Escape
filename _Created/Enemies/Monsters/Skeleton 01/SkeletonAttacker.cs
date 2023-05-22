using UnityEngine;

public class SkeletonAttacker : EnemyAttacker
{
    [SerializeField] Animator _animator;
    public override bool OnEnterAttack()
    {
        if (!_allowAttack) return false;

        _context.agent.isStopped = true;  // stop agent to attack

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

        // rotate to focus at target
        LookAtTarget(_context.player.position);
        return true;
    }
}
