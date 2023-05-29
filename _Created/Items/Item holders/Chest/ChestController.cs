using UnityEngine;

public class ChestController : BaseInteraction
{
    [SerializeField] Animator _animator;
    [SerializeField] ItemStorage _storage;
    public override void Interact()
    {
        _animator.SetTrigger("trigger");

        if(_storage.storage.Count > 0)
        {
            ItemsHolder itemsHolder = _storage.GetHolderByType<SwordController>(3);
            if(itemsHolder != null )
            {
                Debug.Log($"Chest has: {itemsHolder.Amount()} {itemsHolder.TypeItem().name}");
            }
            else
            {
                Debug.Log("holder is null");
            }
        }
    }
}
