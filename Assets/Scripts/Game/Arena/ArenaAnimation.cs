using UnityEngine;

namespace AlienArenas.Game.Arena
{
    public class ArenaAnimation : MonoBehaviour
    {
        private static readonly int IsLowered = Animator.StringToHash("IsLowered");
    
        [SerializeField] private Animator _animator;
    

        private void OnTriggerEnter(Collider other)
        {
            _animator.SetBool(IsLowered, true);
        }
        private void OnTriggerExit(Collider other)
        {
            _animator.SetBool(IsLowered, false);
        }
    }
}
