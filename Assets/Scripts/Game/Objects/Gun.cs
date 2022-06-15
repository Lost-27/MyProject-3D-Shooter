using System.Collections;
using UnityEngine;

namespace AlienArenas.Game.Objects
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _launchPoint;
        [SerializeField] private float _shootDelay = 0.1f;
        [SerializeField] private float _upgradeTime = 10.0f;

        private bool _isUpgraded;
        private AudioSource _audioSource;
        private IEnumerator _fireRoutine;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void UpgradeGun()
        {
            StartCoroutine(UpgradeActionTime());
        }

        public void BeginShooting()
        {
            _fireRoutine = RepeatShooting();
            StartCoroutine(_fireRoutine);
        }

        public void StopShooting()
        {
            StopCoroutine(_fireRoutine);
        }

        private IEnumerator RepeatShooting()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(_shootDelay);
            }
        }

        private IEnumerator UpgradeActionTime()
        {
            _isUpgraded = true;
            yield return new WaitForSeconds(_upgradeTime);
            _isUpgraded = false;
        }

        private void Shoot()
        {
            Rigidbody bullet = СreateBullet();
            bullet.velocity = transform.parent.forward * 100;

            if (_isUpgraded)
            {
                Rigidbody bullet2 = СreateBullet();
                bullet2.velocity = (transform.right + transform.forward / 0.5f) * 50;
                Rigidbody bullet3 = СreateBullet();
                bullet3.velocity = ((transform.right * -1) + transform.forward / 0.5f) * 50;
                _audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
            }

            _audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }

        private Rigidbody СreateBullet()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _launchPoint.position, _launchPoint.rotation);
            return bullet.GetComponent<Rigidbody>();
        }
    }
}