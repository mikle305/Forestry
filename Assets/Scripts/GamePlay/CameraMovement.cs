using Services;
using UnityEngine;

namespace GamePlay
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10;
        
        private InputService _inputService;


        private void Start()
        {
            _inputService = InputService.Instance;
        }

        private void Update()
        {
            Vector2 moveDirection = _inputService.GetMoveDirection();
            transform.position += (Vector3) (_moveSpeed * Time.deltaTime * moveDirection);
        }
    }
}