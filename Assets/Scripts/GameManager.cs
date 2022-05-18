using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int _aliensOnScreen = 0;
    private float _generatedSpawnTime = 0;
    private float _currentSpawnTime = 0;

    private void Start()
    {
    }

    private void Update()
    {
    }
}
