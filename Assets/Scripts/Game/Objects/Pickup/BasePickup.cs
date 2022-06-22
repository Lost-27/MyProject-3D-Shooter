using AlienArenas.Game.Utility.Constants;
using UnityEngine;

namespace AlienArenas.Game.Objects.Pickup
{
    public abstract class BasePickup : MonoBehaviour
    {
        [Header("Base Settings")] 
        [SerializeField] protected float _activeTime;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Player))
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
                ApplyPickup();
                Destroy(gameObject);
            }
        }

        protected abstract void ApplyPickup();
    }
}