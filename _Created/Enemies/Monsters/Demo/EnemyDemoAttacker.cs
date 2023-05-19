using UnityEngine;
using UnityEngine.AI;

public class EnemyDemoAttacker : EnemyAttacker
{
    [SerializeField] Animator _animator;
    public override bool OnEnterAttack()
    {
        if (!_allowAttack) return false;

        attackComplete = false;
        StartCoroutine(StartWaitCoolDown());
        _animator.SetTrigger(EnemyAniParameter.hit);
        return true;
    }

    public override bool OnExitAttack()
    {
        return true;
    }

    public override bool OnStayAttack()
    {
        throw new System.NotImplementedException();
    }
}
