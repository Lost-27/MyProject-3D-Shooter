using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 50.0f;
    [SerializeField] private float _rotateSpeed = 10.0f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Rigidbody _headRb;
    public LayerMask layerMask;

    private Vector3 _currentLookTarget = Vector3.zero;
    private Vector3 _targetPosition;

    private void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _characterController.SimpleMove(moveDirection * _moveSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            // TODO
        }
        else
        {
            _headRb.AddForce(transform.right * 150, ForceMode.Acceleration);
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
}