using UnityEngine;

namespace AlienArenas.Game.Enemy
{
    public class SelfDestruct : MonoBehaviour
    {
        [SerializeField] private float _destructTime = 3.0f;
    
        public void Initiate()
        {
            Invoke(nameof(DestructHead), _destructTime);
        }
        private void DestructHead()
        {
            Destroy(gameObject);
        }
    }
}
