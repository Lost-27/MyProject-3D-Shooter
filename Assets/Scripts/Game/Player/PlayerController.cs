using AlienArenas.Game.Cam;
using AlienArenas.Game.Enemy;
using AlienArenas.Game.Objects;
using UnityEngine;

namespace AlienArenas.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");


        [SerializeField] private Rigidbody _headRb;
        [SerializeField] private Animator _animator;

        [Header("Setting delay hit")] 
        [SerializeField] private float[] hitForce;

        [SerializeField] private float _timeBetweenHits = 2.5f;

        [Header("Setting death")]
        [SerializeField] private Rigidbody _bodyRb;

        [SerializeField] private DeathParticles _deathParticles;

        private bool _isHit;
        private float _timeSinceHit;
        private int _hitNumber = -1;
        private bool _isDead;

        private void Update()
        {
            if (_isHit)
            {
                _timeSinceHit += Time.deltaTime;
                if (_timeSinceHit > _timeBetweenHits)
                {
                    _isHit = false;
                    _timeSinceHit = 0;
                }
            }
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0,
                UnityEngine.Input.GetAxis("Vertical"));
            if (moveDirection == Vector3.zero)
            {
                _animator.SetBool(IsMoving, false);
            }
            else
            {
                _headRb.AddForce(transform.right * 150, ForceMode.Acceleration);
                _animator.SetBool(IsMoving, true);
            }

        }

        void OnTriggerEnter(Collider other)
        {
            AlienBeetle alienBeetle = other.gameObject.GetComponent<AlienBeetle>();
            if (alienBeetle != null)
            {
                // 1
                if (!_isHit)
                {
                    _hitNumber += 1; // 2
                    CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                    if (_hitNumber < hitForce.Length) // 3
                    {
                        cameraShake.intensity = hitForce[_hitNumber];
                        cameraShake.Shake();
                    }
                    else
                    {
                        Die();
                    }

                    _isHit = true; // 4
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
                }

                alienBeetle.Die();
            }
        }

        public void Die()
        {
            _animator.SetBool(IsMoving, false);
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
        }
    }
}