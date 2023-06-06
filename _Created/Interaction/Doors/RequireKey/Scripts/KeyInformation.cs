
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "key", menuName = "Item/Key")]
public class KeyInformation: BaseItem
{
    public override bool Use()
    {
        bool openSuccessful = false;
        string contentNotice = null;
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
            }
        }
        Notice notice = new Notice() { type = TypeNotice.log, content = contentNotice};
        UIWindowManager.instance.ShowNotice(notice);
        return openSuccessful;
    }
}