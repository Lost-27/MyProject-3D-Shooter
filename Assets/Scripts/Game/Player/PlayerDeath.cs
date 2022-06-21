using System;
using AlienArenas.Game.Objects;
using UnityEngine;

namespace AlienArenas.Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Rigidbody _headRb;
        [SerializeField] private Rigidbody _bodyRb;
        [SerializeField] private DeathParticles _deathParticles;
        public event Action OnDeath;
        public bool IsPlayerDeath { get; private set; }

        private void OnEnable()
        {
            _playerHealth.OnChanged += HealthChanged;
        }

        private void OnDisable()
        {
            _playerHealth.OnChanged -= HealthChanged;
        }


        private void HealthChanged()
        {
            if (!IsPlayerDeath && _playerHealth.CurrentHp < 1)
                Die();
        }

        private void Die()
        {
            IsPlayerDeath = true;
            _bodyRb.transform.parent = null;
            _bodyRb.isKinematic = false;
            _bodyRb.useGravity = true;
            _bodyRb.gameObject.GetComponent<CapsuleCollider>().enabled = true;
            _bodyRb.gameObject.GetComponent<Gun>().enabled = false;
            Destroy(_headRb.gameObject.GetComponent<HingeJoint>());
            _headRb.transform.parent = null;
            _headRb.useGravity = true;
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);
            _deathParticles.Activate();
            Destroy(gameObject);
            
            OnDeath?.Invoke();
        }
    }
}