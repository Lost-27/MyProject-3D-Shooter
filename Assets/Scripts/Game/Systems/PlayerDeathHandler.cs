using AlienArenas.Game.Player;
using AlienArenas.Game.UI;
using UnityEngine;

namespace AlienArenas.Game.Systems
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private DeathScreen _deathScreen;
        [SerializeField] private PlayerDeath _playerDeath;


        private void OnEnable()
        {
            _playerDeath.OnDeath += PlayerDead;
        }

        private void OnDisable()
        {
            _playerDeath.OnDeath -= PlayerDead;
        }


        private void PlayerDead()
        {
            _deathScreen.SetActive(true);
        }
    }
}