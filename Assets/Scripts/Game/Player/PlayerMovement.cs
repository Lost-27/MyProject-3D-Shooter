using AlienArenas.Game.Input;
using UnityEngine;
using Zenject;

namespace AlienArenas.Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private CharacterController _controller;

        [Header("Movement Settings")] 
        [SerializeField] private float _moveSpeed = 50.0f;

        [Header("Rotation Settings")] 
        [SerializeField] private float _rotateSpeed = 10.0f;
        [SerializeField] private LayerMask _layerMask;

        private IInputService _inputService;
        private Camera _camera;
        private Transform _cachedTransform;
        private Vector3 _currentLookTarget;
        private Vector3 _targetPosition;
        
        public Vector3 MoveDirection { get; private set; }

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _camera = Camera.main;
            _cachedTransform = transform;
        }

        private void Update()
        {
            Move();
        }

        private void FixedUpdate()
        {
            Rotation();
        }

        public void ResetMove()
        {
            MoveDirection = Vector3.zero;
        }

        private void Move()
        {
            Vector2 moveAxis = _inputService.MoveAxis;
            MoveDirection = new Vector3(moveAxis.x, 0, moveAxis.y) * _moveSpeed;
            _controller.SimpleMove(MoveDirection);
        }

        private void Rotation()
        {
            RaycastHit hit;
            Vector3 mousePosition = _inputService.MousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

            if (Physics.Raycast(ray, out hit, 1000, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.point != _currentLookTarget)
                {
                    _currentLookTarget = hit.point;
                    _targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                    Quaternion rotation = Quaternion.LookRotation(_targetPosition - _cachedTransform.position);

                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotateSpeed);
                }
            }
        }
    }
}