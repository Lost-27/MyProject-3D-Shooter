using UnityEngine;

namespace AlienArenas.Game.Objects
{
    public class Upgrade : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Gun gun = other.GetComponentInChildren<Gun>();
            gun.UpgradeGun();
            Destroy(gameObject);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
        }
    }
}