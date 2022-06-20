using UnityEngine;

namespace AlienArenas.Game.Objects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private GameObject _hitEffect;

        private void OnCollisionEnter(Collision collision)
        {
            GameObject hitEffect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            ParticleSystem tinyExplosion = hitEffect.GetComponent<ParticleSystem>();
            tinyExplosion.Play();
            Destroy(hitEffect,3f);
            Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
