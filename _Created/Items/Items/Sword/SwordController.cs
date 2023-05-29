using UnityEngine;

[CreateAssetMenu(fileName = "Sword info", menuName = "Item/Weapon/Sword")]
public class SwordController: BaseItem
{
    public override void Use()
    {
        Debug.Log("Slash...");
    }
}
