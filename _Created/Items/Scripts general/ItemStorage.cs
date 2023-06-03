using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    [SerializeField] protected List<ItemsHolder> allHolders; // only for display in inspector
    public Dictionary<string, ItemsHolder> storage { get; private set; } // only exchange through this dictionary
    protected virtual void Awake()
    {
        SetupStorage();
    }
    protected virtual void SetupStorage()
    {
        storage = new Dictionary<string, ItemsHolder>();
        foreach(ItemsHolder holder in allHolders)
        {
            storage.Add(holder.TypeItem().key, holder);
        }
    }
    public virtual bool AddHolder(ItemsHolder newHolder)
    {
        if (newHolder == null) return false;

        ItemsHolder holderChecking;
        string key = newHolder.TypeItem().key;

        if (storage.TryGetValue(key, out holderChecking))
        {
            // has exist this key
            holderChecking.Insert(newHolder);

            storage.Remove(key);

            storage.Add(key, holderChecking);
            return true;
        }
        else
        {
            // has not exist this key
            storage.Add(newHolder.TypeItem().key, newHolder);
            return true;
        }
    }
    public virtual ItemsHolder GetHolderByType<T>() where T : BaseItem
    {
        foreach(KeyValuePair<string, ItemsHolder> holder in storage)
        {
            if (holder.Value.TypeItem().GetType() != typeof(T)) continue;

            // found this type in storage
            ItemsHolder result = new ItemsHolder(holder.Value.TypeItem(), holder.Value.Amount());
            holder.Value.RemoveAll();
            storage.Remove(holder.Key);
            return result;
        }
        
        // not found this type item in storage
        return null;
    }
    public virtual ItemsHolder GetHolderByType<T>(int amount) where T : BaseItem
    {
        ItemsHolder result;
        foreach (KeyValuePair<string, ItemsHolder> holder in storage)
        {
            if (holder.Value.TypeItem().GetType() != typeof(T)) continue;

            // found this type in storage
            if (holder.Value.Remove(amount))
            {
                // enough for require
                result = new ItemsHolder(holder.Value.TypeItem(), amount);
            }
            else
            {
                // not enough for require
                result = new ItemsHolder(holder.Value.TypeItem(), 0);
            }
            return result;
        }

        // not found this type item in storage
        return null;
    }
    public virtual Dictionary<string, ItemsHolder> GetAllHolder()
    {
        // init to other dictionary
        Dictionary<string, ItemsHolder> result = new Dictionary<string, ItemsHolder>();
        foreach(var holder in storage)
        {
            result.Add(holder.Key, holder.Value);
        }

        this.storage.Clear();
        this.allHolders.Clear();

        return result;
    }
    public virtual void ImportFromOtherStorage(ItemStorage otherStorage)
    {
        Dictionary<string, ItemsHolder> newHolders = otherStorage.GetAllHolder();

        if (newHolders != null)
        {
            foreach(ItemsHolder holder in newHolders.Values)
            {
                Debug.Log($"add new holder for {gameObject.name}");
                this.AddHolder(holder);
            }
        }
    }
}
