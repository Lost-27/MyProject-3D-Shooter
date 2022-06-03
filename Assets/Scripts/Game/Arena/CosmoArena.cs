using AlienArenas.Game.Cam;
using AlienArenas.Game.Player;
using UnityEngine;

namespace AlienArenas.Game.Arena
{
    public class CosmoArena : MonoBehaviour
    {
        private static readonly int OnElevator = Animator.StringToHash("OnElevator");
    
        public GameObject player;
        public Transform elevator;
        [SerializeField] private Animator _arenaAnimator;
        [SerializeField] private SphereCollider _sphereCollider;

        private void OnTriggerEnter(Collider other)
        {
            UnityEngine.Camera.main.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;
            player.transform.parent = elevator.transform;
            player.GetComponent<PlayerMovement>().enabled = false;
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
            _arenaAnimator.SetBool(OnElevator, true);
        }
    
        public void ActivatePlatform()
        {
            _sphereCollider.enabled = true;
        }
    }
}
