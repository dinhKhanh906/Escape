
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TintModelScene
{
    public class ColorPicker : MonoBehaviour
    {
        public Renderer modelNormal;
        public List<Material> slotsMat;
        public Material slotSelected;
        [SerializeField] Material _defaultMat;
        [SerializeField] PartPicker _currentPartPicker;
        public float intensityColor;
        Material _instanceMat;
        ColorSlot[] _allColorEnabled;
        PartPicker[] _allParts;
        [SerializeField] CameraSetup _camera;
        [SerializeField] TMP_Dropdown _dropdownSlots;
        [SerializeField] Slider _sliderIntensity;
        [ColorUsage(true, true)][SerializeField] Color _colorSelected; // this color is original (intensity = 0)
        ColorSlot _currentColorSlot;
        ModelManager _modelManager;
        public Color ColorSelected { get => _colorSelected; }
        public ColorSlot CurrentColorSlot 
        { 
            get => _currentColorSlot; 
            set 
            { 
                _currentColorSlot = value;
            } 
        }
        public PartPicker CurrentPartPicker
        {
            get => _currentPartPicker;
            set
            {
                this._currentPartPicker = value;
                Color hdrColor = _instanceMat.GetColor(value.nameParameter);
                // Get original color with ZERO intensity
                _colorSelected = HDRColor.GetOriginalColor(hdrColor);
                // set value of slider follow currentIntensity
                _sliderIntensity.value = HDRColor.GetIntensity(hdrColor);
            }
        }
        private void Awake()
        {
            // instantiate a material for display
            _instanceMat = new Material(modelNormal.material);

            modelNormal.sharedMaterial = _instanceMat;
        }
        private void OnEnable()
        {
            _modelManager = ModelManager.instance;
            if (_modelManager)
            {
                slotsMat = _modelManager.GetAllSlotsMaterial();
                _defaultMat = _modelManager.DefaultMaterial;
                ChangeSlotMaterial(0);
            }
            // setup slider values
            if (_sliderIntensity)
            {
                intensityColor = _sliderIntensity.value;
                _sliderIntensity.onValueChanged.AddListener((x) =>
                {
                    intensityColor = x;
                    if (_currentPartPicker)
                    {
                        // update intensity
                        Color hdrColor = _instanceMat.GetColor(_currentPartPicker.nameParameter);
                        // set color intensity
                        HDRColor.SetIntensity(ref hdrColor, x);
                        // update color while sliding
                        _instanceMat.SetColor(_currentPartPicker.nameParameter, hdrColor);
                    }
                });
            }
            // setup dropdown values
            if (_dropdownSlots)
            {
                // setdefault value for dropdown and slotselected
                _dropdownSlots.value = 0;
                slotSelected = slotsMat[0];
                // setup dropdown
                _dropdownSlots.onValueChanged.AddListener((x) =>
                {
                    ChangeSlotMaterial(x);
                });
            }
            // setup instance material
            if (_instanceMat == null)
            {
                _instanceMat = new Material(slotSelected);
            }
        }
        public void SetColorForCurrentPart(Color newColor)
        {
            if (_currentPartPicker != null)
            {
                Color hdrColor = newColor;
                HDRColor.SetIntensity(ref hdrColor, intensityColor);
                _instanceMat.SetColor(_currentPartPicker.nameParameter, hdrColor);
                _colorSelected = newColor;
            }
        }
        public void SetColorForSpecificPart(string nameParameter, Color newColor)
        {
            float factor = Mathf.Pow(2, intensityColor);
            Color hdrColor = _instanceMat.GetColor(nameParameter);
            hdrColor.r = newColor.r * factor;
            hdrColor.b = newColor.b * factor;
            hdrColor.g = newColor.g * factor;
            _instanceMat.SetColor(nameParameter, hdrColor);
            _colorSelected = newColor;
        }
        public void ChangeSlotMaterial(int index)
        {
            slotSelected = slotsMat[index];
            // change current material of model
            _instanceMat = new Material(slotSelected);
            modelNormal.material = _instanceMat;
        }
        public void RandomColor()
        {
            // get all color can selecte
            if (_allColorEnabled == null || _allColorEnabled.Length <= 0)
            {
                _allColorEnabled = FindObjectsOfType<ColorSlot>();
            }
            // get all parts
            if (_allParts == null || _allParts.Length <= 0)
            {
                _allParts = FindObjectsOfType<PartPicker>();
            }
            // disable focus camera
            if (!_camera) _camera = FindObjectOfType<CameraSetup>();
            _camera.Unfocus();
            // random color for each part of character
            foreach (PartPicker part in _allParts)
            {
                int indexColor = Random.Range(0, _allColorEnabled.Length);
                SetColorForSpecificPart(part.nameParameter, _allColorEnabled[indexColor].GetColor());
            }
        }
        public void BackToDefaultColor()
        {
            if (_defaultMat)
            {
                _instanceMat = new Material(_defaultMat);
                modelNormal.material = _instanceMat;
            }
        }
        public void SaveChanged()
        {
            if (_instanceMat == null || slotSelected == null) return;

            _modelManager.SaveValueOfSlot(_dropdownSlots.value, _instanceMat);
        }

        public void RemoveChanged()
        {
            // re-clone current slot selected for instance material
            _instanceMat = new Material(slotSelected);
            modelNormal.material = _instanceMat;
        }
    }
}