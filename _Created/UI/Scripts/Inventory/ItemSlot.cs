

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour
{
    public ItemsHolder itemHolder;
    public BaseButton btnTrigger;
    public TMP_Text tmpAmount;
    public Image avatar;
    public Image border;
    public Color tintHighLight;
    public void Debut(ItemsHolder itemHolder)
    {
        this.itemHolder = itemHolder;
        avatar.sprite = itemHolder.TypeItem().avatar;
        tmpAmount.text = itemHolder.Amount().ToString();
    }
    public void HighLight()
    {
        border.color = tintHighLight;
    }
    public void UnHighLight()
    {
        border.color = Color.white;
    }
    public void Use(int amount)
    {
        bool successful = false;
        // use single
        if (amount == 1)
        {
            successful = itemHolder.UseSingle();
        }
        else
        {
            // use multiple
            successful = itemHolder.UseMultiple(amount);
            if(!successful) Debug.LogWarning($"{itemHolder.TypeItem().nameItem} Not enough");
        }

        if (!successful) return;

        // check amount remaining
        if(itemHolder.Amount() <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            tmpAmount.text = itemHolder.Amount().ToString();
        }
    }
}