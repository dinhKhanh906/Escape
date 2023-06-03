
using UnityEngine;
using System.Diagnostics;

public class PlayerStorage: ItemStorage
{
    public bool hasChangedElements { get; private set; }
    public InventoryWindow window;
    public PlayerUIInput input;
    protected override void Awake()
    {
        base.Awake();
        window.playerStorage = this;
    }
    private void Start()
    {
        hasChangedElements = true;
        window.CloseWindow();
    }
    private void Update()
    {
        if (input.inventory)
        {
            bool enabled = window.isActiveAndEnabled;
            if (enabled) window.CloseWindow();
            else
            {
                window.ShowWindow(false);
                // nothing changed
                hasChangedElements = false;
            }
        }
    }
    public override bool AddHolder(ItemsHolder newHolder)
    {
        bool result = base.AddHolder(newHolder);

        if (result) hasChangedElements = true;

        return result;
    }
    public override ItemsHolder GetHolderByType<T>()
    {
        ItemsHolder result = base.GetHolderByType<T>();

        if (result != null) hasChangedElements = true;

        return result;
    }
    public override ItemsHolder GetHolderByType<T>(int amount)
    {
        ItemsHolder result = base.GetHolderByType<T>(amount);

        if(result != null) hasChangedElements = true;

        return result;
    }
}