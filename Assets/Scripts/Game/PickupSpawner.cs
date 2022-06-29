using AlienArenas.Game.Enemy;
using AlienArenas.Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArenas.Game
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private GameObject[] _pickupPrefab;

        [Range(0f, 100f)]
        [SerializeField] private float _pickupChance;

        //[Header("Components")]
        //[SerializeField] private EnemyDeath _enemyDeath;



        private int _numbeEnemiesKilled;


        private void Start()
        {
            //_enemyDeath.OnDeath.AddListener(SpawnPickup);
        }

        private void Update()
        {
            
        }


        private void SpawnPickup()
        {
            _numbeEnemiesKilled++;

            if (_numbeEnemiesKilled == 5)
            {
                _numbeEnemiesKilled = 0;

                Transform spawnLocation = GetRandomSpawnLocation();

                CreatePickupIfNeeded(spawnLocation.position);
            }
        }


        private void CreatePickupIfNeeded(Vector3 spawnPointPosition)
        {
            float randomChance = Random.Range(1f, 100f);

            if (!(_pickupChance > randomChance))
                return;

            Instantiate(GetRandomPickup(), spawnPointPosition, Quaternion.identity);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
        }


        private GameObject GetRandomPickup()
        {
            int randomIndex = Random.Range(0, _pickupPrefab.Length);
            return _pickupPrefab[randomIndex];
        }

        private Transform GetRandomSpawnLocation()
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomIndex];
        }

    }
}
