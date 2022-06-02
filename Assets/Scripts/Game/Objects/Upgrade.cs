using UnityEngine;

namespace AlienArenas.Game.Objects
{
    public class Upgrade : MonoBehaviour
    {
        public Gun gun;
        private void OnTriggerEnter(Collider other)
        {
            gun.UpgradeGun();
            Destroy(gameObject);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
        }
    }
}
