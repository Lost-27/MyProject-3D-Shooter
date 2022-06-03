using UnityEngine;

namespace AlienArenas.Game.Cam
{
    public class CameraMovement : MonoBehaviour
    {
        public GameObject followTarget;
        public float moveSpeed;

        private void Update()
        {
            if (followTarget != null)
            {
                transform.position = Vector3.Lerp(transform.position, followTarget.transform.position,
                    Time.deltaTime * moveSpeed);
            }
        }
    }
}