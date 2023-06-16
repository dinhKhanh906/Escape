
using UnityEngine;
using UnityEngine.UI;

namespace TintModelScene
{
    public class ColorSlot : MonoBehaviour
    {
        public ButtonHighlight button;
        [SerializeField] Image imageColor;
        [SerializeField] Color _color;

        ColorPicker _colorPicker;
        public Color GetColor() => this._color;
        private void Reset()
        {
            if(imageColor == null) imageColor = transform.Find("Background").GetComponent<Image>();
            if (button == null) button = transform.Find("Button").GetComponent<ButtonHighlight>();
        }
        private void Awake()
        {
            _colorPicker = FindObjectOfType<ColorPicker>();
            if (imageColor) _color = imageColor.color;
        }
        private void OnEnable()
        {
            button.onClick.AddListener(() =>
            {
                if(_colorPicker != null)
                {
                    _colorPicker.SetColorForCurrentPart(_color);
                    if(_colorPicker.CurrentColorSlot == null)
                    {
                        this.button.Highlight();
                        _colorPicker.CurrentColorSlot = this;
                    }
                    else if(_colorPicker.CurrentColorSlot != this)
                    {
                        _colorPicker.CurrentColorSlot.button.UnHighlight();
                        this.button.Highlight();
                        _colorPicker.CurrentColorSlot = this;
                    }
                }
            });
        }
    }
}