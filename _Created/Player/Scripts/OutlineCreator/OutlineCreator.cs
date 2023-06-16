using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutlineCreator : MonoBehaviour
{
    [SerializeField] Material _outlineMat;
    [SerializeField] OutlineTarget _currentTarget;
    [SerializeField] PlayerDetector _detector;

    private void Start()
    {
        _detector.onCurrentTargetChanged.AddListener(() =>
        {
            RemoveOutlineCurrentTarget();

            if(_detector.currentTarget) SetOutline(_detector.currentTarget.outlineTarget);
        });
    }
    public void SetOutline(OutlineTarget target)
    {
        if (target == null)
        {
            _currentTarget = null;
            return;
        }
        if (target.renderersRequire.Length <= 0) return;

        foreach (Renderer renderer in target.renderersRequire)
        {
            List<Material> newMaterials = new List<Material>();
            newMaterials.AddRange(renderer.sharedMaterials);
            newMaterials.Add(_outlineMat);

            renderer.sharedMaterials = newMaterials.ToArray();
        }
        _currentTarget = target;
    }
    public void RemoveOutlineCurrentTarget()
    {
        if (_currentTarget == null) return;
        if (_currentTarget.renderersRequire.Length <= 0) return;

        foreach (Renderer renderer in _currentTarget.renderersRequire)
        {
            List<Material> newMaterials = new List<Material>();
            
            foreach(Material mat in renderer.sharedMaterials)
            {
                if(mat != _outlineMat)
                    newMaterials.Add(mat);
            }

            renderer.sharedMaterials = newMaterials.ToArray();
        }
    }
}
