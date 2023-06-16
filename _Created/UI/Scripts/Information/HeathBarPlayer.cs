using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBarPlayer : MonoBehaviour
{
    public Slider sliderHeath;
    [SerializeField] PlayerInformation _player;
    private void Awake()
    {
        if(_player == null) _player = FindObjectOfType<PlayerInformation>();
        sliderHeath.maxValue = _player.Heath;
        sliderHeath.value = _player.Heath;
        sliderHeath.minValue = 0f;
    }
    private void OnEnable()
    {
        _player.onHeathChanged.AddListener(() =>
        {
            // update max value of slider if player's hp > current max
            if(sliderHeath.maxValue < _player.Heath) sliderHeath.maxValue = _player.Heath;
            // update value
            sliderHeath.value = _player.Heath;
        });
    }
}
