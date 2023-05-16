using UnityEngine;

public class Enemy : BaseInforInteraction, IInteraction
{
    public override void SetType()
    {
        type = TypeOfInteraction.ENEMY;
    }
    public void Interact()
    {
        Debug.Log("hit enemy");
    }
}
