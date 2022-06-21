using System.Collections;
using AlienArenas.Game.Enemy;
using AlienArenas.Game.Utility.Constants;
using UnityEngine;

namespace AlienArenas.Game.Objects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _lifeTime;
        [SerializeField] private GameObject _hitEffect;

        private IEnumerator _despawnBulletRoutine;

        private void OnEnable()
        {
            _despawnBulletRoutine = DespawnBulletByLifeTime();
            StartCoroutine(_despawnBulletRoutine);
        }

        private void OnDisable()
        {
            if (_despawnBulletRoutine != null)
                StopCoroutine(_despawnBulletRoutine);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.Enemy))
            {
                other.GetComponent<EnemyHealth>().TakeDamage(_damage);
            }

            DespawnBullet();

            CreateHitEffect();
        }

        private void OnCollisionEnter()
        {
            CreateHitEffect();

            DespawnBullet();
        }

        private void CreateHitEffect()
        {
            GameObject hitEffect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            ParticleSystem tinyExplosion = hitEffect.GetComponent<ParticleSystem>();
            tinyExplosion.Play();

            Destroy(hitEffect, 2f);
        }

        private IEnumerator DespawnBulletByLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);

            DespawnBullet();
        }

        private void DespawnBullet() =>
            Destroy(gameObject);
    }
}