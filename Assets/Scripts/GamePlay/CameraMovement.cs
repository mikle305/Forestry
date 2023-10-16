using Services;
using UnityEngine;

namespace GamePlay
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 20;
        
        private InputService _inputService;
        private float _speedFactor = 1;


        private void Start()
        {
            _inputService = InputService.Instance;
        }

        private void Update()
        {
            if (_inputService == null)
                return;
            
            UpdatePosition();
        }

        public void SetSpeedFactor(float factor)
            => _speedFactor = factor;

        private void UpdatePosition()
        {
            
            
            Vector2 moveDirection = _inputService.GetMoveDirection();
            transform.position += (Vector3) (_speed * _speedFactor * Time.deltaTime * moveDirection);
        }
    }
}