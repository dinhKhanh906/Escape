
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "key", menuName = "Item/Key")]
public class KeyInformation: BaseItem
{
    public override bool Use()
    {
        bool openSuccessful = false;
        // unlock the door nearby
        PlayerDetector detector = FindObjectOfType<PlayerDetector>();
        if (detector != null)
        {
            BaseInteraction currentTarget = detector.currentTarget;
            if(currentTarget.GetType() == typeof(DoorRequireKey))
            {
                DoorRequireKey doorTarget = (DoorRequireKey)currentTarget;
                openSuccessful = doorTarget.OpenDoor(this);
            }
        }
        // show result
        if (openSuccessful) Debug.Log("open this door successful");
        else Debug.Log("open this door faild");

        return openSuccessful;
    }
}