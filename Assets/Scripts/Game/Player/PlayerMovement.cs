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

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }


        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 moveAxis = _inputService.MoveAxis;
            Vector3 moveDirection = new Vector3(moveAxis.x, 0, moveAxis.y);
            _controller.SimpleMove(moveDirection * _moveSpeed);
        }
    }
}