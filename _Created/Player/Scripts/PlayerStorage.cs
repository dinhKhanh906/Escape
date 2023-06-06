
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;

public class PlayerStorage: ItemStorage
{
    public bool hasChangedElements;
    private void Start()
    {
        hasChangedElements = true;
    }
    public override bool AddHolder(ItemsHolder newHolder)
    {
        bool result = base.AddHolder(newHolder);

        // notice if player receive new items
        if (result)
        {
            Notice notice = new Notice() { type = TypeNotice.log, content = $"Received new {newHolder.TypeItem().nameItem}" };
            UIWindowManager.instance.ShowNotice(notice);
            hasChangedElements = true;
        }

        return result;
    }
    public override ItemsHolder DropHolderByType<T>()
    {
        ItemsHolder result = base.DropHolderByType<T>();

        if (result != null) hasChangedElements = true;

        return result;
    }
    public override ItemsHolder DropHolderByType<T>(int amount)
    {
        ItemsHolder result = base.DropHolderByType<T>(amount);

        if(result != null) hasChangedElements = true;

        return result;
    }
    public virtual void ImportFromOtherStorage(ItemStorage otherStorage)
    {
        Dictionary<string, ItemsHolder> newHolders = otherStorage.DropAllHolder();
        string noticeContent = "Added:";
        if (newHolders != null)
        {
            foreach (ItemsHolder holder in newHolders.Values)
            {
                noticeContent += $"{holder.Amount()} ({holder.TypeItem().nameItem})s, ";
                this.AddHolder(holder);
            }
        }
        Notice notice = new Notice() { type = TypeNotice.log, content = noticeContent };
        UIWindowManager.instance.ShowNotice(notice);
    }
}