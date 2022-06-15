using AlienArenas.Game.Core;
using Zenject;

namespace AlienArenas.Game.Player.Installers
{
    public class PlayerHealthInstaller : MonoInstaller
    {
        public PlayerHealth PlayerHealth;
        public override void InstallBindings()
        {
            Container.Bind<IHealth>().To<PlayerHealth>().FromInstance(PlayerHealth);
        }
    }
}
