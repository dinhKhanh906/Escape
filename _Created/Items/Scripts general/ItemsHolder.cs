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
    public bool Use(int amount)
    {
        // can not use if amount greater current amount
        if (this.amount < amount) return false;

        // use
        for(int i=0; i<amount; i++)
        {
            typeItem.Use();
            this.amount--;
        }
        return true;
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
