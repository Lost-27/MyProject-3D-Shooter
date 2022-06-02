using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticles;
    private bool _didStart;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void Update()
    {
        if (_didStart && _deathParticles.isStopped)
        {
            Destroy(gameObject);
        }
    }
    
    public void Activate()
    {
        _didStart = true;
        _deathParticles.Play();
    }
    
    public void SetDeathFloor(GameObject deathFloor)
    {
        if (_deathParticles == null)
        {
            _deathParticles = GetComponent<ParticleSystem>();
        }
        _deathParticles.collision.SetPlane(0, deathFloor.transform);
    }
}
