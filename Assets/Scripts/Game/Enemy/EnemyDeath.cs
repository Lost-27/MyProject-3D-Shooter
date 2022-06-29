using System;
using UnityEngine;
using UnityEngine.Events;

namespace AlienArenas.Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Rigidbody _headRb;
        [SerializeField] private DeathParticles _deathParticles;
        [SerializeField] private EnemyPickupSpawner _enemyPickup;
        public UnityEvent OnDeath;
        

        private EnemyHealth _enemyHealth;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.OnChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _enemyHealth.OnChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_enemyHealth.CurrentHp > 0)
                return;

            Die();
        }

        private void Die()
        {
            _headRb.GetComponent<Animator>().enabled = false;
            _headRb.isKinematic = false;
            _headRb.useGravity = true;
            _headRb.GetComponent<SphereCollider>().enabled = true;
            _headRb.gameObject.transform.parent = null;
            _headRb.velocity = new Vector3(0, 26.0f, 3.0f);
            OnDeath.Invoke();
            OnDeath.RemoveAllListeners();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
            _headRb.GetComponent<SelfDestruct>().Initiate();
            if (_deathParticles)
            {
                _deathParticles.transform.parent = null;
                _deathParticles.Activate();
            }
            _enemyPickup.Spawn();

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
}