using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBeetle : MonoBehaviour
{
    public Transform _target;
    public UnityEvent OnDestroy;
    public bool isAlive = true;

    [Header("Setting death")] 
    [SerializeField] private Rigidbody _headRb;
    [SerializeField] private DeathParticles _deathParticles;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _navigationUpdate;

    private float _navigationTime;

    private void Update()
    {
        if (isAlive)
        {
            if (_target != null)
            {
                _navigationTime += Time.deltaTime;
                if (_navigationTime > _navigationUpdate)
                {
                    _agent.destination = _target.position;
                    _navigationTime = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        _headRb.GetComponent<Animator>().enabled = false;
        _headRb.isKinematic = false;
        _headRb.useGravity = true;
        _headRb.GetComponent<SphereCollider>().enabled = true;
        _headRb.gameObject.transform.parent = null;
        _headRb.velocity = new Vector3(0, 26.0f, 3.0f);
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        _headRb.GetComponent<SelfDestruct>().Initiate();
        if (_deathParticles)
        {
            _deathParticles.transform.parent = null;
            _deathParticles.Activate();
        }

        Destroy(gameObject);
    }

    public DeathParticles GetDeathParticles()
    {
        if (_deathParticles == null)
        {
            _deathParticles = GetComponentInChildren<DeathParticles>();
        }

        return _deathParticles;
    }
}