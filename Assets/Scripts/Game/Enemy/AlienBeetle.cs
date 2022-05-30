using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBeetle : MonoBehaviour
{
    public Transform _target;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float navigationUpdate;
    
    private float _navigationTime = 0;


    private void Update()
    {
        _navigationTime += Time.deltaTime;
        if (_navigationTime > navigationUpdate)
        {
            _agent.destination = _target.position;
            _navigationTime = 0;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
    }
}