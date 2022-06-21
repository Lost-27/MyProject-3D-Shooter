using System;
using AlienArenas.Game.Core;
using UnityEngine;

namespace AlienArenas.Game.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxHp;
        public int CurrentHp { get; private set; }

        public event Action OnChanged;

        private void Awake()
        {
            CurrentHp = _maxHp;            
            OnChanged?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            if (CurrentHp <= 0)
                return;

            CurrentHp -= damage;
            OnChanged?.Invoke();
        }
    }
}
