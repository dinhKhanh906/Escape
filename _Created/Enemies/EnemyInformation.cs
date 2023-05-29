
using UnityEngine;

public class EnemyInformation: BaseInteraction
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
        Debug.Log($"interacted to enemy: {gameObject.name}");
    }
}