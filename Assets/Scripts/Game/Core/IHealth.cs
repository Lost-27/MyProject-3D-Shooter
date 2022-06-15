using System;

namespace AlienArenas.Game.Core
{
    public interface IHealth
    {
        event Action OnChanged;

        int CurrentHp { get; }
        // int MaxHp { get; }
        //
        // void Setup(int current, int max);
        // void AddLife(int healthPoints);
    }
}