using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "heath", menuName = "Item/Medicine/Heath")]
public class Heath : BaseItem
{
    public float amountHeath;
    public override bool Use()
    {
        PlayerInformation player = FindObjectOfType<PlayerInformation>();
        if (player)
        {
            player.Heath += amountHeath;
            return true;
        }
        else 
            return false;
    }
}
