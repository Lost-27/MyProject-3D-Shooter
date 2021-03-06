using UnityEngine;
using UnityEngine.AI;

namespace AlienArenas.Game.Enemy
{
    public class AlienBeetle : MonoBehaviour
    {
        public Transform _target;
        public bool isAlive = true;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _navigationUpdate;

        private float _navigationTime;

        private void Update()
        {
            if (isAlive)
            {
                if (_target != null)
                {
                    _navigationTime += Time.deltaTime;
                    if (_navigationTime > _navigationUpdate)
                    {
                        _agent.destination = _target.position;
                        _navigationTime = 0;
                    }
                }
            }
        }
    }
}