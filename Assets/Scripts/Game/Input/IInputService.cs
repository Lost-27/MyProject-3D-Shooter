using UnityEngine;

namespace AlienArenas.Game.Input
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }

        void SetLocked(bool isLocked);
    }
}