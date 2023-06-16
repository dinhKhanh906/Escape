
using UnityEngine;
using UnityEngine.UI;

public class LoadingController: MonoBehaviour
{
    [SerializeField] Slider _sliderProgress;
    private float _progress;
    private void OnEnable()
    {
        _sliderProgress.onValueChanged.AddListener((x) =>
        {
            SetValueBar(x);
        });
    }
    private void OnDisable()
    {
        _sliderProgress.onValueChanged.RemoveListener(SetValueBar);
    }
    private void SetValueBar(float value)
    {
        _sliderProgress.value = value;
    }
    public float Progress
    {
        get => _sliderProgress.value;
        set {
            _sliderProgress.value = value;
        }
    }
}