using AlienArenas.Game.Player;
using UnityEngine;

namespace AlienArenas.Game.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            PlayerDelayHit playerDelayHit = other.gameObject.GetComponent<PlayerDelayHit>();
            
            if (playerHealth == null)
                return;
            
            if (!playerDelayHit.IsHit)
            {
                playerHealth.TakeDamage(_damage);
            }
        }
    }
}