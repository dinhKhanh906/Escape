using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAttackEvent : MonoBehaviour
{
    public LayerMask enemyLayer;
    public PlayerStateMachine context;
    [Header("View collide setting")]
    [SerializeField] bool _overViewColider;
    [SerializeField] Vector3 _centerOffset;
    [SerializeField] float _radiusColide;
    [Header("View move forward setting")]
    [SerializeField] bool _overViewMove;
    [SerializeField] float _distanceForwad;
    private void OnDrawGizmosSelected()
    {
        if (_overViewColider)
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + transform.forward * _centerOffset.z + transform.right * _centerOffset.x + transform.up * _centerOffset.y;
            Gizmos.DrawWireSphere(center, _radiusColide);
        }
        if (_overViewMove)
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

        Vector3 targetPosition = context.transform.position + context.transform.forward * distance;
        StartCoroutine(MoveToTargetPoint(targetPosition, duration));
    }
    private IEnumerator MoveToTargetPoint(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = context.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t);

            context.character.Move((currentPosition - transform.position).normalized * context.moveSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // make sure move to exactly point
        context.character.Move((targetPosition - transform.position).normalized * context.moveSpeed * Time.deltaTime);
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
                Debug.Log(col.transform.root.gameObject.name);
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
