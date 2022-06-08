using UnityEngine;

namespace AlienArenas
{
    public class DeathParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _deathParticles;

        private bool _didStart;

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
}