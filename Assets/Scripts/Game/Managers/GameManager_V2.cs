using System;
using System.Collections.Generic;
using AlienArenas.Game.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AlienArenas.Game.Managers
{
    public class GameManager_V2 : MonoBehaviour
    {
        private static readonly int PlayerWon = Animator.StringToHash("PlayerWon");

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject[] _enemyPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Animator _arenaAnimator;

        [Header("Pickup settings")]
        [SerializeField] private GameObject[] _pickupPrefab;

        [Range(1, 50)]
        [SerializeField] private int _maxEnemiesKilled;

        [Range(0f, 100f)]
        [SerializeField] private float _pickupChance;

        [Header("Enemy spawn settings")]
        [SerializeField] private int _maxAliensOnScreen;
        [SerializeField] private int _totalAliens;
        [SerializeField] private float _minSpawnTime;
        [SerializeField] private float _maxSpawnTime;

        [Range(1, 10)]
        [SerializeField] private int _aliensPerSpawn;

        public GameObject deathFloor;

        private int _aliensOnScreen;
        private float _generatedSpawnTime;
        private float _currentSpawnTime;
        private int _numberEnemiesKilled;
        private List<int> _previousSpawnLocations;
        
        public int TotalAliens => _totalAliens;

        
        private void Update()
        {
            if (_player == null)
            {
                return;
            }

            SpawnerEnemies();
        }


        private void SpawnerEnemies()
        {
            _currentSpawnTime += Time.deltaTime;

            if (_currentSpawnTime > _generatedSpawnTime)
            {
                _currentSpawnTime = 0;
                _generatedSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);

                if (_aliensPerSpawn > 0 && _aliensOnScreen < _totalAliens)
                {
                    _previousSpawnLocations = new List<int>();

                    if (_aliensPerSpawn > _spawnPoints.Length)
                    {
                        _aliensPerSpawn = _spawnPoints.Length - 1;
                    }                    

                    if (_aliensPerSpawn > _totalAliens)
                    {
                        int aliensPerSpawn1 = _aliensPerSpawn - _totalAliens;

                        if (aliensPerSpawn1 > 1)
                        {
                            _aliensPerSpawn = 1;
                        }
                        else
                        {
                            _aliensPerSpawn = aliensPerSpawn1;
                        }
                    }


                    for (int i = 0; i < _aliensPerSpawn; i++)
                    {
                        if (_aliensOnScreen < _maxAliensOnScreen)
                        {
                            _aliensOnScreen += 1;

                            // 1
                            int index = -1;

                            while (index == -1)
                            {
                                int randomNumber = Random.Range(0, _spawnPoints.Length);

                                if (!_previousSpawnLocations.Contains(randomNumber))
                                {
                                    _previousSpawnLocations.Add(randomNumber);
                                    index = randomNumber;
                                }
                            }

                            Transform spawnLocation = _spawnPoints[index];
                            GameObject newAlienBeetle = CreateEnemy(spawnLocation.position);


                            AlienBeetle alienBeetle = newAlienBeetle.GetComponent<AlienBeetle>();
                            alienBeetle._target = _player.transform;
                            var position = _player.transform.position;
                            var targetRotation = new Vector3(position.x, newAlienBeetle.transform.position.y,
                                position.z);
                            newAlienBeetle.transform.LookAt(targetRotation);

                            EnemyDeath enemyDeath = newAlienBeetle.GetComponent<EnemyDeath>();
                            enemyDeath.OnDeath.AddListener(AlienDestroyed);
                            enemyDeath.GetDeathParticles().SetDeathFloor(deathFloor);

                        }
                    }
                }
            }
        }


        private void AlienDestroyed()
        {
            _aliensOnScreen -= 1;
            _totalAliens -= 1;

            SpawnPickup();

            if (_totalAliens == 0)
            {
                Invoke(nameof(EndGame), 2.0f);
            }
        }


        private void EndGame()
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
            _arenaAnimator.SetTrigger(PlayerWon);
        }


        private GameObject CreateEnemy(Vector3 spawnPointPosition) =>
            Instantiate(GetRandomEnemy(), spawnPointPosition, Quaternion.identity);


        private GameObject GetRandomEnemy()
        {
            int randomIndex = Random.Range(0, _enemyPrefab.Length);
            return _enemyPrefab[randomIndex];
        }


        private void SpawnPickup()
        {
            _numberEnemiesKilled++;

            if (_numberEnemiesKilled == _maxEnemiesKilled)
            {
                _numberEnemiesKilled = 0;

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