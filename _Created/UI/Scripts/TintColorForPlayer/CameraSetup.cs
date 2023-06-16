
using TintModelScene;
using TMPro;
using UnityEngine;

namespace TintModelScene
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] Animator _playerPose;
        CameraViewerControl _cameraCtrl;
        ColorPicker _colorPicker;
        public BaseButton btnFocus;
        public BaseButton btnUnfocus;
        private void Awake()
        {
            _cameraCtrl = FindObjectOfType<CameraViewerControl>();
            _colorPicker = FindObjectOfType<ColorPicker>();
        }
        private void OnEnable()
        {
            if (_cameraCtrl)
            {
                btnFocus.onClick.AddListener(() =>
                {
                    Focus();
                });
                btnUnfocus.onClick.AddListener(() =>
                {
                    Unfocus();
                });
            }

            btnFocus.gameObject.SetActive(true);
            btnUnfocus.gameObject.SetActive(false);
        }
        public void Focus()
        {
            PartPicker part = _colorPicker.CurrentPartPicker;
            if (part != null)
            {
                _cameraCtrl.EnableCameraFocus(part.cam, part.AllowRotate());
                btnUnfocus.gameObject.SetActive(true);
                btnFocus.gameObject.SetActive(false);

                // switch to T-pose
                if (_playerPose) _playerPose.SetBool("isTPose", true);
            }
        }
        public void Unfocus()
        {
            PartPicker part = _colorPicker.CurrentPartPicker;
            if (part != null)
            {
                _cameraCtrl.DisableCameraFocus();
                btnUnfocus.gameObject.SetActive(false);
                btnFocus.gameObject.SetActive(true);

                // switch to normal-pose
                if (_playerPose) _playerPose.SetBool("isTPose", false);
            }
        }
    }
}