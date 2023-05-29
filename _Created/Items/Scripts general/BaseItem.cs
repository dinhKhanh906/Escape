using UnityEngine;

public abstract class BaseItem: ScriptableObject
{
    public string key;
    public string nameItem;
    public Sprite avatar;
    public string description;

    public abstract void Use();
}
