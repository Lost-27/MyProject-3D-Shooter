using System.Collections.Generic;
using UnityEngine;

namespace AlienArenas.Game.Enemy
{
    public class EnemyPickupSpawner : MonoBehaviour
    {
        #region Variables

        [SerializeField] private List<GameObject> _pickupPrefab;


        [Range(0f, 100f)]
        [SerializeField] private float _pickupChance;

        #endregion


        #region Public methods

        public void Spawn()
        {
            float randomChance = Random.Range(1f, 100f);

            if (_pickupChance > randomChance)
            {
                Instantiate(GetRandomPickupPrefab(), transform.position, Quaternion.identity);
            }
        }

        #endregion


        #region Private methods

        private GameObject GetRandomPickupPrefab()
        {
            return _pickupPrefab[Random.Range(0, _pickupPrefab.Count)];
        }

        #endregion
    }
}