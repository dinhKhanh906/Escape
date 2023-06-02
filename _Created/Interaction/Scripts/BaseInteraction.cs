
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour, IInteraction
{
    [SerializeField] protected bool previewDistanceRequire;
    public float distanceRequireInteract;

    protected Transform _player;
    protected float _distance2Player;
    protected virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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