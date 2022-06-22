using AlienArenas.Game.Player;
using UnityEngine;

namespace AlienArenas.Game.Objects.Pickup
{
    public class PickupHealth : MonoBehaviour
    {
        [SerializeField] private int _healthPoints = 1;

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.AddHP(_healthPoints);
            Destroy(gameObject);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
        }
    }
}
