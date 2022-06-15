using System.Collections.Generic;
using AlienArenas.Game.Core;
using UnityEngine;
using Zenject;

namespace AlienArenas.Game.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject _heartCellPrefab;
        [SerializeField] private Transform _healthBar;

        private IHealth _health;
        private List<GameObject> _maxHearts = new List<GameObject>();

        [Inject]
        public void Construct(IHealth health)
        {
            _health = health;
        }

        private void Start()
        {
            _health.OnChanged += UpdateCells;

            InstantiateHP();
            UpdateCells();
        }

        private void OnDestroy()
        {
            _health.OnChanged -= UpdateCells;
        }

        private void InstantiateHP()
        {
            for (int i = 0; i < _health.CurrentHp; i++)
            {
                GameObject heart = Instantiate(_heartCellPrefab, _healthBar);
                _maxHearts.Add(heart);
            }
        }

        private void UpdateCells()
        {
            for (int i = 0; i < _maxHearts.Count; i++)
            {
                GameObject heart = _maxHearts[i];
                bool isActive = _health.CurrentHp > i;
                heart.SetActive(isActive);
            }
        }
    }
}