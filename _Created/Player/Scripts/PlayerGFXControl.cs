
using UnityEngine;

public class PlayerGFXControl: MonoBehaviour
{
    [SerializeField] Renderer _renderer;

    private void Start()
    {
        ModelManager modelManager = ModelManager.instance;
        if (modelManager)
        {
            SetMaterial(modelManager.CurrentMaterial);
        }
    }
    public void SetMaterial(Material material)
    {
        if (_renderer)
        {
            _renderer.sharedMaterial = material;
        }
    }
}