
using UnityEngine;

public class EnemyController: BaseInteraction
{
    public float heath;
    public float speed;
    public float damage;

    protected override void Awake()
    {
        base.Awake();
        distanceRequireInteract = +Mathf.Infinity;
    }
    public override void Interact()
    {
        
    }
}