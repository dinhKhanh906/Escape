using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HomeScene
{
    public class ModelSetup : MonoBehaviour
    {
        public TMP_Dropdown dropdownSlots;
        [SerializeField] Material[] _matSlots;
        ModelManager _modelManager;

        private void Start()
        {
            // set up material when game start
            if (_modelManager == null) _modelManager = ModelManager.instance;
            if (_modelManager.CurrentMaterial == null)
            {
                _modelManager.CurrentMaterial = _matSlots[0];
            }
        }
        private void OnEnable()
        {
            ModelManager modelManager = ModelManager.instance;
            if (modelManager)
            {
                _matSlots = modelManager.GetAllSlotsMaterial().ToArray();
                OnSlotChanged(0);
            }
            if (dropdownSlots)
            {
                dropdownSlots.onValueChanged.AddListener(x => OnSlotChanged(x));
            }
        }
        private void OnDisable()
        {
            if (dropdownSlots)
                dropdownSlots.onValueChanged.RemoveListener(x => OnSlotChanged(x));
        }
        public void OnSlotChanged(int index)
        {
            if(!_modelManager) _modelManager = ModelManager.instance;
            if (_modelManager)
            {
                Material matInstance = _matSlots[index];
                _modelManager.CurrentMaterial = matInstance;
            }
            else
            {
                Debug.LogWarning("Not found ModelManager & make sure slot changed not null");
            }
        }
    }

}