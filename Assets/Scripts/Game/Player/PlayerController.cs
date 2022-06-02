using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    public LayerMask layerMask;

    [SerializeField] private float _moveSpeed = 50.0f;
    [SerializeField] private float _rotateSpeed = 10.0f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _headRb;
    [SerializeField] private Animator _animator;

    [Header("Setting delay hit")]
    [SerializeField] private float[] hitForce;
    [SerializeField] private float _timeBetweenHits = 2.5f;
    
    [Header("Setting death")]
    [SerializeField] private Rigidbody _bodyRb;
    

    private Vector3 _currentLookTarget = Vector3.zero;
    private Vector3 _targetPosition;
    private bool _isHit;
    private float _timeSinceHit;
    private int _hitNumber = -1;
    private bool _isDead = false;

    private void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.SimpleMove(moveDirection * _moveSpeed);
        
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
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            _animator.SetBool(IsMoving, false);
        }
        else
        {
            _headRb.AddForce(transform.right * 150, ForceMode.Acceleration);
            _animator.SetBool(IsMoving, true);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);


        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != _currentLookTarget)
            {
                _currentLookTarget = hit.point;
                _targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                Quaternion rotation = Quaternion.LookRotation(_targetPosition - transform.position);

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotateSpeed);
            }
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
        Destroy(gameObject);
    }
}