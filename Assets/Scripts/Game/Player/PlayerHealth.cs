using System;
using AlienArenas.Game.Cam;
using AlienArenas.Game.Core;
using AlienArenas.Game.Enemy;
using UnityEngine;

namespace AlienArenas.Game.Player
{
    public class PlayerHealth : MonoBehaviour,IHealth
    {
        [SerializeField] private int _maxHp;
        [SerializeField] private float[] _hitForce;

        private bool _isDead;
        private Camera _camera;
        
        public event Action OnChanged;
        public int CurrentHp { get; private set; }

        private void Awake()
        {
            CurrentHp = _maxHp;
            _camera = Camera.main;
        }

        public void TakeDamage(int damage)
        {
            if (CurrentHp < 1)
                return;

            CurrentHp -= damage;
            CurrentHp = Mathf.Max(0, CurrentHp);
            CameraShake cameraShake = _camera.GetComponent<CameraShake>();
            cameraShake.intensity = _hitForce[CurrentHp];
            cameraShake.Shake();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);

            OnChanged?.Invoke();
        }
    }
}