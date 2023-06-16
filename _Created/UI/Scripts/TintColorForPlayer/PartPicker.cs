
using Cinemachine;
using UnityEngine;

namespace TintModelScene
{
    public class PartPicker : MonoBehaviour
    {
        [SerializeField] bool _allowRotate;
        public ButtonHighlight button;
        public string nameParameter;
        public CinemachineVirtualCamera cam;

        ColorPicker _colorPicker;
        CameraViewerControl _camViewer;
        public bool AllowRotate() => _allowRotate;
        private void Awake()
        {
            _colorPicker = FindObjectOfType<ColorPicker>();
            _camViewer = FindObjectOfType<CameraViewerControl>();
            button = GetComponentInChildren<ButtonHighlight>();
        }
        private void OnEnable()
        {

            button.onClick.AddListener(() =>
            {
                if(_colorPicker.CurrentPartPicker == null)
                {
                    // update to this
                    _colorPicker.CurrentPartPicker = this;
                    button.Highlight();
                }
                else if(_colorPicker.CurrentPartPicker != this)
                {
                    // un highlight old part picker
                    _colorPicker.CurrentPartPicker.button.UnHighlight();
                    // update to this
                    _colorPicker.CurrentPartPicker = this;
                    button.Highlight();
                    // focus this part if viewer is focus
                    if (_camViewer.isFocus) _camViewer.EnableCameraFocus(this.cam, _allowRotate);
                }
            });
        }
    }
}