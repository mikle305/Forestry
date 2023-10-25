using Cinemachine;
using Services;
using UnityEngine;

namespace GamePlay
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private float _minSize = 5;
        [SerializeField] private float _maxSize = 15;
        [SerializeField] private float _zoomSpeed = 1;

        private float _defaultSize;
        private InputService _inputService;


        private void Start()
        {
            _inputService = InputService.Instance;
            _defaultSize = _virtualCamera.m_Lens.OrthographicSize;
        }

        private void Update()
        {
            if (_inputService == null)
                return;
            
            float zoomCoefficient = UpdateZoom();
            _cameraMovement.SetSpeedFactor(zoomCoefficient);
        }
        
        /// <summary></summary>
        /// <returns>Coefficient of zooming</returns>
        private float UpdateZoom()
        {
            float zoomDirection = _inputService.GetZoomDirection();
            float sizeDelta = zoomDirection * _zoomSpeed * Time.deltaTime;
            float expectedSize = _virtualCamera.m_Lens.OrthographicSize + sizeDelta;
            float targetSize = GetTargetZoomSize(expectedSize);
            _virtualCamera.m_Lens.OrthographicSize = targetSize;
            return targetSize / _defaultSize;
        }

        private float GetTargetZoomSize(float expectedSize)
        {
            float targetSize;
            if(expectedSize < _minSize)
                targetSize = _minSize;
            else if(expectedSize > _maxSize)
                targetSize = _maxSize;
            else
                targetSize = expectedSize;

            return targetSize;
        }
    }
}