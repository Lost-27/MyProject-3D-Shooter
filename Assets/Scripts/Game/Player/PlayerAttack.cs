using AlienArenas.Game.Input;
using AlienArenas.Game.Objects;
using UnityEngine;
using Zenject;

namespace AlienArenas.Game.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Gun _gun;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (_inputService.IsButtonAttackDown)
            {
                _gun.BeginShooting();
            }

            else if (_inputService.IsButtonAttackUp)
            {
                _gun.StopShooting();
            }
        }
    }
}