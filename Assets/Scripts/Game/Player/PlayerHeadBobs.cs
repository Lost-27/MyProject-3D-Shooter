using AlienArenas.Game.Cam;
using AlienArenas.Game.Enemy;
using AlienArenas.Game.Objects;
using UnityEngine;

namespace AlienArenas.Game.Player
{
    public class PlayerHeadBobs : MonoBehaviour
    {
        [SerializeField] private Rigidbody _headRb;
        [SerializeField] private PlayerMovement _playerMovement;

        private void FixedUpdate()
        {
            if (_playerMovement.MoveDirection != Vector3.zero)
            {
                _headRb.AddForce(transform.right * 150, ForceMode.Acceleration);
            }
        }
    }
}