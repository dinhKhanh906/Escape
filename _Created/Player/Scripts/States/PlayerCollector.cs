using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] PlayerStorage storage;
    [SerializeField] int itemDropLayer;
    private void OnTriggerEnter(Collider other)
    {
        // collect item drop
        if(other.gameObject.layer == itemDropLayer)
        {
            // get item profile
            ItemsHolder holder = other.GetComponent<ItemDrop>().Pick();
            // add into storage
            if(holder != null )
            {
                storage.AddHolder(holder);
            }
        }
    }
}