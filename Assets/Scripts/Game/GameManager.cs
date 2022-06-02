using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject _alienBeetle;

    public int maxAliensOnScreen;
    public int totalAliens;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;
    public GameObject upgradePrefab;
    public Gun gun;
    public float upgradeMaxTimeSpawn = 7.5f;

    private int _aliensOnScreen = 0;
    private float _generatedSpawnTime = 0;
    private float _currentSpawnTime = 0;
    private bool _spawnedUpgrade = false;
    private float _actualUpgradeTime = 0;
    private float _currentUpgradeTime = 0;

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
        _currentSpawnTime += Time.deltaTime;
        if (_currentUpgradeTime > _actualUpgradeTime)
        {
            // 1
            if (!_spawnedUpgrade)
            {
                // 2
                int randomNumber = Random.Range(0, _spawnPoints.Length - 1);
                GameObject spawnLocation = _spawnPoints[randomNumber];
                // 3
                GameObject upgrade = Instantiate(upgradePrefab);
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;
                // 4
                _spawnedUpgrade = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
            }
        }

        if (_currentSpawnTime > _generatedSpawnTime)
        {
            _currentSpawnTime = 0;
            _generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            if (aliensPerSpawn > 0 && _aliensOnScreen < totalAliens)
            {
                List<int> previousSpawnLocations = new List<int>();

                if (aliensPerSpawn > _spawnPoints.Length)
                {
                    aliensPerSpawn = _spawnPoints.Length - 1;
                }

                aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn;

                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    if (_aliensOnScreen < maxAliensOnScreen)
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
                        Vector3 targetRotation = new Vector3(_player.transform.position.x, newAlienBeetle.transform.position.y, _player.transform.position.z);
                        newAlienBeetle.transform.LookAt(targetRotation);
                        alienBeetle.OnDestroy.AddListener(AlienDestroyed);
                    }
                }
            }
        }
    }
    
    private void AlienDestroyed()
    {
        _aliensOnScreen -= 1;
        totalAliens -= 1;
    }
}