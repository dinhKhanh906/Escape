
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "key", menuName = "Item/Key")]
public class KeyInformation: BaseItem
{
    public override bool Use()
    {
        bool openSuccessful = false;
        TypeNotice noticeType = TypeNotice.warning;
        string contentNotice = "make sure target is a door";
        // unlock the door nearby
        PlayerDetector detector = FindObjectOfType<PlayerDetector>();
        if (detector != null)
        {
            BaseInteraction currentTarget = detector.currentTarget;
            if(currentTarget == null)
            {
                openSuccessful = false;
                contentNotice = "Not found any target";
            }
            else if (!currentTarget.AllowInteract())
            {
                contentNotice = "target is too far to try open";
            }
            else if(currentTarget.GetType() == typeof(DoorRequireKey))
            {
                DoorRequireKey doorTarget = (DoorRequireKey)currentTarget;
                openSuccessful = doorTarget.OpenDoor(this);

                // get result
                contentNotice = openSuccessful ? "Open door target successful !" : "This key incorrect door target";
                noticeType = openSuccessful ? TypeNotice.log : TypeNotice.warning;
            }
        }
        Notice notice = new Notice() { type = noticeType, content = contentNotice};
        UIWindowManager.instance.ShowNotice(notice);
        return openSuccessful;
    }
}