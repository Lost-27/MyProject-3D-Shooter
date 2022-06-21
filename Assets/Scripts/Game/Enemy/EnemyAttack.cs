using AlienArenas.Game.Player;
using UnityEngine;

namespace AlienArenas.Game.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth == null)
                return;
            
            playerHealth.TakeDamage(_damage);
        }
    }
}