
using UnityEngine;

[RequireComponent(typeof(OutlineTarget))]
public abstract class BaseInteraction : MonoBehaviour, IInteraction
{
    [SerializeField] protected bool previewDistanceRequire;
    public float distanceRequireInteract;
    public OutlineTarget outlineTarget;

    protected Transform _player;
    protected float _distance2Player;
    private void Reset()
    {
        outlineTarget = GetComponent<OutlineTarget>();
    }
    protected virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if(outlineTarget == null) outlineTarget = GetComponent<OutlineTarget>();
    }
    protected virtual void OnDrawGizmosSelected()
    {
        if (previewDistanceRequire)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distanceRequireInteract);
        }
    }
    public abstract void Interact();
    public virtual bool AllowInteract()
    {
        _distance2Player = Vector3.Distance(transform.position, _player.position);
        if (_distance2Player <= distanceRequireInteract) return true;
        else return false;
    }
}