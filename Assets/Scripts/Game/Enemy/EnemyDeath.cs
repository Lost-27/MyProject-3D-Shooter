using UnityEngine;
using UnityEngine.Events;

namespace AlienArenas.Game.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Rigidbody _headRb;
        [SerializeField] private DeathParticles _deathParticles;
        public UnityEvent OnDestroy;
        public bool isAlive = true;


        public void Die()
        {
            isAlive = false;
            _headRb.GetComponent<Animator>().enabled = false;
            _headRb.isKinematic = false;
            _headRb.useGravity = true;
            _headRb.GetComponent<SphereCollider>().enabled = true;
            _headRb.gameObject.transform.parent = null;
            _headRb.velocity = new Vector3(0, 26.0f, 3.0f);
            OnDestroy.Invoke();
            OnDestroy.RemoveAllListeners();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
            _headRb.GetComponent<SelfDestruct>().Initiate();
            if (_deathParticles)
            {
                _deathParticles.transform.parent = null;
                _deathParticles.Activate();
            }

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