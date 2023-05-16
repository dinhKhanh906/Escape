
using UnityEngine;

public abstract class BaseInforInteraction: MonoBehaviour
{
    [HideInInspector] public TypeOfInteraction type;
    public abstract void SetType();
    protected virtual void Awake()
    {
        SetType();
    }
}
