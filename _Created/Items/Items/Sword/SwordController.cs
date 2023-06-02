using UnityEngine;

[CreateAssetMenu(fileName = "Sword info", menuName = "Item/Weapon/Sword")]
public class SwordController: BaseItem
{
    public override bool Use()
    {
        Debug.Log("Slash...");
        return true;
    }
}
