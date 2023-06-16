using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttackEvent : MonoBehaviour
{
    public LayerMask enemyLayer;
    public PlayerStateMachine context;
    public PlayerInformation playerInfor;
    [Header("View hit collider setting")]
    [SerializeField] bool _preViewColider;
    [SerializeField] Vector3 _centerOffset;
    [SerializeField] float _radiusColide;
    [Header("View hit move forward setting")]
    [SerializeField] bool _preViewMove;
    [SerializeField] float _distanceForwad;
    private void OnDrawGizmosSelected()
    {
        if (_preViewColider)
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + transform.forward * _centerOffset.z + transform.right * _centerOffset.x + transform.up * _centerOffset.y;
            Gizmos.DrawWireSphere(center, _radiusColide);
        }
        if (_preViewMove)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(context.transform.position + context.transform.forward * _distanceForwad, 0.2f);
        }
    }
    public void AttackMoveForward(string value)
    {
        // form input: "distance, duration"
        char[] delimiterChars = { ',', ':', '\t' };

        string[] values = value.Split(delimiterChars);
        float distance = float.Parse(values[0]);
        float duration = float.Parse(values[1]);

        // focus target
        context.LookAtTarget();

        Vector3 targetPosition = context.transform.position + context.transform.forward * distance;
        StartCoroutine(context.MoveToTargetPoint(targetPosition, duration));
    }
    public void HitForward(string value)
    {
        // form input: "xOffset, yOffset, zOffset, radius"
        char[] delimiterChars = { ',', ':', '\t' };

        string[] values = value.Split(delimiterChars);

        // get values
        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        Vector3 center = transform.position + transform.forward * z + transform.right * x + transform.up * y;
        float radius = float.Parse(values[3]);

        //
        Collider[] enemyCols = Physics.OverlapSphere(center, radius, enemyLayer);
        if (enemyCols == null) return;
        if(enemyCols.Length > 0)
        {
            foreach(Collider col in enemyCols)
            {
                EnemyCollider enemy = col.GetComponent<EnemyCollider>();
                enemy.ReceiveDame(playerInfor.Damage);
            }
        }
    }
    public void CheckContinueCombo()
    {
        var meleeAttack = context.currentState;
        if (meleeAttack.GetType() == typeof(PlayerMeleeAttack)) (
                (PlayerMeleeAttack)meleeAttack).listening = true;
    }
}
