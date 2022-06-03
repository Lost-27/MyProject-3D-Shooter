using UnityEngine;

namespace AlienArenas.Game.Input
{
    public class StandaloneInputService : IInputService
    {
        private bool _isLocked;

        public Vector2 MoveAxis
        {
            get
            {
                if (_isLocked)
                    return Vector2.zero;

                return new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            }
        }

        public void SetLocked(bool isLocked) =>
            _isLocked = isLocked;
    }
}