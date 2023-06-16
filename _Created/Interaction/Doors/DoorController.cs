
using UnityEngine;
using UnityEngine.Events;

public class DoorController: BaseInteraction
{
    [SerializeField] protected bool _wasInteracted;
    public UnityEvent onOpenSite;
    protected override void Awake()
    {
        base.Awake();
        _wasInteracted = false;
    }
    public override void Interact()
    {

    }
    public virtual void OpenSite()
    {
        // only invoke onOpenSite event one time
        if (!_wasInteracted)
        {
            onOpenSite?.Invoke();
            _wasInteracted = true;
        }
    }
}