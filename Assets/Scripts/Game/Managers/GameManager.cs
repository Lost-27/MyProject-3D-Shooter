using System.Collections.Generic;
using AlienArenas.Game.Enemy;
using AlienArenas.Game.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AlienArenas.Game
{
    public class GameManager : MonoBehaviour
    {
        private static readonly int PlayerWon = Animator.StringToHash("PlayerWon");

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject[] _spawnPoints;
        [SerializeField] private GameObject _alienBeetle;
        [SerializeField] private Animator _arenaAnimator;

        public int MaxAliensOnScreen;
        public int TotalAliens;
        public float MinSpawnTime;
        public float MaxSpawnTime;
        public int AliensPerSpawn;
        public GameObject upgradePrefab;
        public GameObject deathFloor;
        public float upgradeMaxTimeSpawn = 7.5f;

        private int _aliensOnScreen;
        private float _generatedSpawnTime;
        private float _currentSpawnTime;
        private bool _spawnedUpgrade;
        private float _actualUpgradeTime;
        private float _currentUpgradeTime;

        private void Start()
        {
            _actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f, upgradeMaxTimeSpawn);
            _actualUpgradeTime = Mathf.Abs(_actualUpgradeTime);
        }

        private void Update()
        {
            if (_player == null)
            {
                return;
            }

            _currentUpgradeTime += Time.deltaTime;


            if (_currentUpgradeTime > _actualUpgradeTime)
            {
                _currentUpgradeTime = 0;
                // 1
                if (!_spawnedUpgrade)
                {
                    // 2
                    int randomNumber = Random.Range(0, _spawnPoints.Length - 1);
                    GameObject spawnLocation = _spawnPoints[randomNumber];
                    // 3
                    GameObject upgrade = Instantiate(upgradePrefab);
                    upgrade.transform.position = spawnLocation.transform.position;
                    // 4
                    _spawnedUpgrade = true;
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
                }
            }

            SpawnerEnemies();
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
                            Vector3 targetRotation = new Vector3(_player.transform.position.x,
                                newAlienBeetle.transform.position.y, _player.transform.position.z);
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
                Invoke(nameof(EndGame), 2.0f);
            }
        }

        private void EndGame()
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
            _arenaAnimator.SetTrigger(PlayerWon);
        }
    }
}