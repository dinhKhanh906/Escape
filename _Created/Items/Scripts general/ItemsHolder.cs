using UnityEngine;
using System;

[System.Serializable]
public class ItemsHolder 
{
    [SerializeField] BaseItem typeItem;
    [SerializeField] int amount;
    public ItemsHolder(BaseItem typeItem, int amount)
    {
        this.typeItem = typeItem;
        this.amount = amount;
    }
    public BaseItem TypeItem() => typeItem;
    public int Amount() => amount;
    public bool UseMultiple(int amount)
    {
        string contentNotice = null;
        // constraint only for type use multiple item
        if (typeItem.onlyUseSingle) return false;

        // can not use if amount greater current amount
        if (this.amount < amount) return false;

        // use
        for(int i=0; i<amount; i++)
        {
            typeItem.Use();
            this.amount--;
        }

        contentNotice = $"Used {amount} ({typeItem.name})s";

        // display result
        UIWindowManager.instance.ShowNotice(new Notice() { type = TypeNotice.log, content = contentNotice});
        return true;
    }
    public bool UseSingle()
    {
        bool successful = typeItem.Use();
        if (successful) 
        {
            this.amount--;

            UIWindowManager.instance.ShowNotice(new Notice() { type = TypeNotice.log, content = $"Used a {typeItem.nameItem}" });
        }
        return successful;
    }
    public bool Remove(int amount)
    {
        if(this.amount < amount) return false;
        else
        {
            this.amount -= amount;
            return true;
        }
    }
    public void RemoveAll() => this.amount = 0;
    public bool Insert(ItemsHolder otherHolder)
    {
        // add faild when type not same
        if (otherHolder.typeItem.GetType() != this.typeItem.GetType()) return false;

        // add
        this.amount += otherHolder.amount;
        return true;
    }
}
