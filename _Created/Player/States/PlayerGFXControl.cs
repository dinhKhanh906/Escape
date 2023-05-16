using UnityEngine;

public class PlayerGFXControl: MonoBehaviour
{
    public Transform model;
    private void Awake()
    {
        if (model == null) model = transform.GetChild(0);
    }
    private void FixedUpdate()
    {
        transform.rotation = model.localRotation;
        transform.position = model.localPosition;
    }
}