using UnityEngine;

namespace AlienArenas.Game.Input
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }

        Vector3 MousePosition { get; }

        bool IsButtonAttackDown { get; }
        bool IsButtonAttackUp { get; }

        void SetLocked(bool isLocked);
    }
}