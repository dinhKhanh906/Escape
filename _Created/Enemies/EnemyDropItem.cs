
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem: ItemStorage
{
    [SerializeField] GameObject itemDropPrefab;
    float maxForceDrop = 150f;
    float minForceDrop = 100f;
    public void DropItems()
    {
        Dictionary<string, ItemsHolder> itemsDrop = this.DropAllHolder();
        foreach(ItemsHolder itemsHolder in itemsDrop.Values)
        {
            InstantiateItem(itemsHolder);
        }
    }
    protected virtual void InstantiateItem(ItemsHolder itemHolder)
    {
        // instantiate item drop
        GameObject newItem = Instantiate(itemDropPrefab, transform.position + Vector3.up, Quaternion.identity);
        // setup info of this item
        ItemDrop dropper = newItem.GetComponent<ItemDrop>();
        dropper.SetHolder(itemHolder);

        // throw it out
        float forceDrop = Random.Range(minForceDrop, maxForceDrop);
        dropper.Spawn(itemHolder.TypeItem().avatar, forceDrop);
    }
}