using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRequireKey : BaseInteraction
{
    [SerializeField] Animator _animator;
    [SerializeField] KeyInformation _keyRequire;
    public bool isUnlocked { get; private set; }
    public override void Interact()
    {
        if (!isUnlocked)
        {
            Notice notice = new Notice() { type = TypeNotice.warning, content = "This door is still locked" };
            UIWindowManager.instance.ShowNotice(notice);
        }
        else
        {
            _animator.SetTrigger("trigger");
        }
    }
    public bool OpenDoor(KeyInformation keyInput)
    {
        if (keyInput == _keyRequire)
        {
            isUnlocked = true;
            return true;
        }
        else
            return false;
    }
}
