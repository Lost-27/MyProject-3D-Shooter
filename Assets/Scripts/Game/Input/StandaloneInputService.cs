namespace AlienArenas.Game.Input
{
    using UnityEngine;

    public class StandaloneInputService : IInputService
    {
        private bool _isLocked;

        public Vector2 MoveAxis
        {
            get
            {
                if (_isLocked)
                    return Vector2.zero;

                return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
        }

        public Vector3 MousePosition
        {
            get
            {
                if (_isLocked)
                    return Vector3.zero;

                return Input.mousePosition;
            }
        }

        public bool IsButtonAttackDown => !_isLocked && Input.GetButtonDown("Fire1");
        public bool IsButtonAttackUp => !_isLocked && Input.GetButtonUp("Fire1");

        public void SetLocked(bool isLocked) =>
            _isLocked = isLocked;
    }
}