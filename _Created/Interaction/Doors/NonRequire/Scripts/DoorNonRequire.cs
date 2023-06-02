using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNonRequire : BaseInteraction
{
    [SerializeField] Animator _animator;
    public override void Interact()
    {
        _animator.SetTrigger("trigger");
    }
}
