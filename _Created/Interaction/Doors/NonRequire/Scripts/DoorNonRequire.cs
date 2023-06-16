using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNonRequire : DoorController
{
    [SerializeField] Animator _animator;
    public override void Interact()
    {
        base.Interact();
        OpenSite();
        _animator.SetTrigger("trigger");
    }
}
