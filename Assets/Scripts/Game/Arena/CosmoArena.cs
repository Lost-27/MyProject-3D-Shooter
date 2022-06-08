using AlienArenas.Game.Cam;
using AlienArenas.Game.Objects;
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

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(player == null)
                return;
            
            _camera.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;
            player.transform.parent = elevator.transform;
            
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.ResetMove();
            playerMovement.enabled = false;
            
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
            _arenaAnimator.SetBool(OnElevator, true);
        }

        public void ActivatePlatform()
        {
            _sphereCollider.enabled = true;
        }
    }
}