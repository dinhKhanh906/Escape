

using UnityEngine;

[RequireComponent(typeof(Animator))]
// this class has all events that was attached in attack animation
public class EnemyAttackEvent: MonoBehaviour
{
    public EnemyAttacker attacker;
    private void Awake()
    {
        if (attacker == null) Debug.LogWarning($"{transform.root.gameObject.name}: Attacker in EnemyEvent is null");
    }
    public void EnterAttack()
    {
        attacker.OnEnterAttack();
    }
    public void FinishedAttack()
    {
        attacker.attackComplete = true;
        attacker.OnExitAttack();
    }
    public void Hit()
    {

    }
}
