

using UnityEngine;

[RequireComponent(typeof(Animator))]
// this class has all events that was attached in attack animation
public class EnemyAttackEvent: MonoBehaviour
{
    public EnemyAttacker attacker;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] EnemyInformation infor;
    [Header("Hit box preview")]
    [SerializeField] bool _showHitBox;
    [SerializeField] Vector3 _centerOffset;
    [SerializeField] float _radiusColide;
    private void Awake()
    {
        if (attacker == null) Debug.LogWarning($"{transform.root.gameObject.name}: Attacker in EnemyEvent is null");
    }
    private void OnDrawGizmosSelected()
    {
        if (_showHitBox)
        {
            Gizmos.color = Color.red;
            Vector3 center = transform.position + transform.forward * _centerOffset.z + transform.right * _centerOffset.x + transform.up * _centerOffset.y;
            Gizmos.DrawWireSphere(center, _radiusColide);
        }
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
    public void Hit(string value)
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
        Collider[] playerCols = Physics.OverlapSphere(center, radius, playerLayer);
        if (playerCols == null) return;
        if (playerCols.Length > 0)
        {
            foreach (Collider col in playerCols)
            {
                PlayerCollider player = col.GetComponent<PlayerCollider>();
                if (player != null) player.ReceiveDame(infor.damage);
            }
        }
    }
}
