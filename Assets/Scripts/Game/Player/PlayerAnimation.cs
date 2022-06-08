using UnityEngine;

namespace AlienArenas.Game.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimation : MonoBehaviour
    {
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        
        [SerializeField] private Animator _animator;

        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _animator.SetFloat(Velocity, _playerMovement.MoveDirection.magnitude);
        }

    }
}