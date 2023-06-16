using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TintModelScene
{
    public class CameraViewerControl : MonoBehaviour
    {
        [Header("Cameras setup")]
        [SerializeField] Transform _cameraDefault, _currentCamera;
        public bool isFocus;
        [Header("Rotate model setup")]
        public Transform cameraHolder;
        public float rotateSpeed;
        [SerializeField] bool _allowRotate;
        private Vector3 _lastMousePosition;
        private Vector3 _currentMousePosition;
        [SerializeField] bool _isRotating;
        private void Start()
        {
            _allowRotate = true;
        }
        // Update is called once per frame
        void Update()
        {
            if (!_allowRotate) return;

            // rotate when hold left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                // ignore if mouse position start is a ui
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                else
                {
                    _lastMousePosition = Input.mousePosition;
                    _isRotating = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isRotating = false;
            }

            if (_isRotating)
            {
                _currentMousePosition = Input.mousePosition;
                float deltaX = _currentMousePosition.x - _lastMousePosition.x;
                cameraHolder.Rotate(Vector3.up, rotateSpeed * deltaX * Time.deltaTime);
                // update last position
                _lastMousePosition = _currentMousePosition;
            }
        }
        public void EnableCameraFocus(CinemachineVirtualCamera cam, bool isAllowRotate)
        {
            this._allowRotate = isAllowRotate;
            _cameraDefault.gameObject.SetActive(false);
            if (!isAllowRotate)
            {
                cameraHolder.DORotate(Vector3.zero, 0.5f);
            }
            if(_currentCamera != null)
            {
                // disable old cam
                _currentCamera.gameObject.SetActive(false);
            }
            // set to new cam
            cam.gameObject.SetActive(true);
            _currentCamera = cam.transform;
            isFocus = true;
        }
        public void DisableCameraFocus()
        {
            // always allow rotate camera when is non focus
            this._allowRotate = true;
            if(_currentCamera != null )
            {
                _currentCamera.gameObject.SetActive(false);
            }
            // set to default camera
            _cameraDefault.gameObject.SetActive(true);
            _currentCamera = _cameraDefault;

            isFocus = false;
        }
    }
}
