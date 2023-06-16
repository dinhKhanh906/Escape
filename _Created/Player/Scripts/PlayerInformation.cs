
using UnityEngine;
using UnityEngine.Events;

public class PlayerInformation: MonoBehaviour
{
    [SerializeField] float _heath;
    [SerializeField] float _damage;
    public float Heath
    {
        get => _heath;
        set
        {
            _heath = value;
            onHeathChanged?.Invoke();
        }
    }
    public float Damage
    {
        get => _damage;
        set
        {
            _damage = value;
        }
    }
    [HideInInspector] public UnityEvent onHeathChanged = new UnityEvent();
}
