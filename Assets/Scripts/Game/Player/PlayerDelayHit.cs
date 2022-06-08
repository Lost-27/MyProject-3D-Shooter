using System.Collections;
using UnityEngine;

namespace AlienArenas.Game.Player
{
    public class PlayerDelayHit : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenHits = 2.5f;
        [SerializeField] private PlayerHealth _playerHealth;

        public bool IsHit { get; private set; }


        private void OnEnable()
        {
            _playerHealth.OnChanged += HitDelay;
        }

        private void OnDisable()
        {
            _playerHealth.OnChanged -= HitDelay;
        }

        private void HitDelay()
        {
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            IsHit = true;
            yield return new WaitForSeconds(_timeBetweenHits);
            IsHit = false;
        }
    }
}