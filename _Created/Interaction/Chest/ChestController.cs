using System.Collections.Generic;
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
            PlayerStorage player = FindObjectOfType<PlayerStorage>();
            if (player)
            {
                player.ImportFromOtherStorage(this._storage);
            }
        }
    }
}
