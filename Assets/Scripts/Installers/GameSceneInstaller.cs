using AlienArenas.Game.Services.Currency;
using Zenject;

namespace AlienArenas.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            CurrencyServiceInstaller.Install(Container);
            //PauseServiceInstaller.Install(Container);
        }
    }
}