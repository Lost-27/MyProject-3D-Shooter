using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmoArena : MonoBehaviour
{
    private static readonly int OnElevator = Animator.StringToHash("OnElevator");
    
    public GameObject player;
    public Transform elevator;
    [SerializeField] private Animator _arenaAnimator;
    [SerializeField] private SphereCollider _sphereCollider;

    private void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;
        player.transform.parent = elevator.transform;
        player.GetComponent<PlayerController>().enabled = false;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
        _arenaAnimator.SetBool(OnElevator, true);
    }
    
    public void ActivatePlatform()
    {
        _sphereCollider.enabled = true;
    }
}
