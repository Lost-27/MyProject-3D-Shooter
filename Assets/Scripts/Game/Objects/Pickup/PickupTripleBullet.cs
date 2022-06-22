using UnityEngine;

namespace AlienArenas.Game.Objects.Pickup
{
    public class PickupTripleBullet : MonoBehaviour
    {
        [SerializeField] private float _activeTime = 10.0f;
        private void OnTriggerEnter(Collider other)
        {
            Gun gun = other.GetComponentInChildren<Gun>();
            gun.UpgradeGun(_activeTime);
            Destroy(gameObject);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
        }
    }
}