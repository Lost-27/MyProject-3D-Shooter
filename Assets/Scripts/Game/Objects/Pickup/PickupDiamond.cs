using AlienArenas.Game.Player;
using AlienArenas.Game.Utility.Constants;
using UnityEngine;

namespace AlienArenas.Game.Objects.Pickup
{
    public class PickupDiamond : MonoBehaviour
    {
        [SerializeField] private int _pointValue;


        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Player))
                return;

            CollectionCoins collectionCoins = other.GetComponent<CollectionCoins>();
            collectionCoins.AddCoin(_pointValue);

            Destroy(gameObject);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
        }
    }
}
