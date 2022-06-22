using System.Collections;
using System.Collections.Generic;
using AlienArenas.Game.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AlienArenas.Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static readonly int PlayerWon = Animator.StringToHash("PlayerWon");

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _alienBeetle;
        [SerializeField] private GameObject[] _spawnPoints;
        [SerializeField] private Animator _arenaAnimator;

        [Header("Pickup settings")] 
        [SerializeField] private GameObject[] _pickupPrefab;
        [SerializeField] private float _pickupMaxTimeSpawn = 7.5f;

        [Range(0f, 100f)] 
        [SerializeField] private float _pickupChance;

        [Header("Enemy spawn settings")] 
        public int MaxAliensOnScreen;
        public int TotalAliens;
        public float MinSpawnTime;
        public float MaxSpawnTime;
        public int AliensPerSpawn;
        public GameObject deathFloor;

        private int _aliensOnScreen;
        private float _generatedSpawnTime;
        private float _currentSpawnTime;
        
        private float _actualPickupTime;
        private IEnumerator _pickupSpawnRoutine;
        

        private void Start()
        {
            _pickupSpawnRoutine = RepeatPickupSpawn();
            StartCoroutine(_pickupSpawnRoutine);
        }

        private void Update()
        {
            if (_player == null)
            {
                return;
            }

            SpawnerEnemies();
        }

        private IEnumerator RepeatPickupSpawn()
        {
            while (true)
            {
                _actualPickupTime = Random.Range(_pickupMaxTimeSpawn - 3.0f, _pickupMaxTimeSpawn);
                _actualPickupTime = Mathf.Abs(_actualPickupTime);
                
                yield return new WaitForSeconds(_actualPickupTime);

                GameObject spawnLocation = GetRandomSpawnLocation();

                CreatePickupIfNeeded(spawnLocation.transform.position);
            }
        }

        private void SpawnerEnemies()
        {
            _currentSpawnTime += Time.deltaTime;

            if (_currentSpawnTime > _generatedSpawnTime)
            {
                _currentSpawnTime = 0;
                _generatedSpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);

                if (AliensPerSpawn > 0 && _aliensOnScreen < TotalAliens)
                {
                    List<int> previousSpawnLocations = new List<int>();

                    if (AliensPerSpawn > _spawnPoints.Length)
                    {
                        AliensPerSpawn = _spawnPoints.Length - 1;
                    }

                    AliensPerSpawn = (AliensPerSpawn > TotalAliens) ? AliensPerSpawn - TotalAliens : AliensPerSpawn;

                    for (int i = 0; i < AliensPerSpawn; i++)
                    {
                        if (_aliensOnScreen < MaxAliensOnScreen)
                        {
                            _aliensOnScreen += 1;
                            // 1
                            int spawnPoint = -1;
                            // 2
                            while (spawnPoint == -1)
                            {
                                // 3
                                int randomNumber = Random.Range(0, _spawnPoints.Length - 1);
                                // 4
                                if (!previousSpawnLocations.Contains(randomNumber))
                                {
                                    previousSpawnLocations.Add(randomNumber);
                                    spawnPoint = randomNumber;
                                }
                            }

                            GameObject spawnLocation = _spawnPoints[spawnPoint];
                            GameObject newAlienBeetle = Instantiate(_alienBeetle);
                            newAlienBeetle.transform.position = spawnLocation.transform.position;

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
            TotalAliens -= 1;

            if (TotalAliens == 0)
            {
                StopCoroutine(_pickupSpawnRoutine);
                Invoke(nameof(EndGame), 2.0f);
            }
        }

        private void EndGame()
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
            _arenaAnimator.SetTrigger(PlayerWon);
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
        

        private GameObject GetRandomSpawnLocation()
        {
            int randomNumber = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[randomNumber];
        }
    }
}